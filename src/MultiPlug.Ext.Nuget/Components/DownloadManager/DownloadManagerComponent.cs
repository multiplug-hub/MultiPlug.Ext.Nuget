using System;
using System.Net;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Nuget.Models.Components.DownloadManager;
using MultiPlug.Extension.Core;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Threading.Tasks;
using NuGet.Versioning;

namespace MultiPlug.Ext.Nuget.Components.Download
{
    class DownloadManagerComponent : DownloadManagerProperties
    {
        private IMultiPlugActions m_MultiPlugActionsAPI;


        private ItemProgress[] m_Progress = new ItemProgress[0];

        private string m_DownloadDirectory;
        private string m_CurrentMultiPlugVersion;

        public bool PermissionsErrorInstall { get; private set; }
        public bool PermissionsErrorRestart { get; private set; }
        public bool RestartRequired { get; private set; }

        public DownloadManagerComponent(IMultiPlugActions theMultiPlugActions, string theCurrentMultiPlugVersion)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            m_MultiPlugActionsAPI = theMultiPlugActions;

            m_CurrentMultiPlugVersion = theCurrentMultiPlugVersion;

            m_DownloadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "downloads");

            DownloadProgressEvent = new Event { Guid = Guid.NewGuid().ToString(), Id = Guid.NewGuid().ToString(), Description = "Download Progress", Subjects = new string[] { "Guid", "Percentage", "Progress" } };
            NotificationEvent = new Event { Guid = Guid.NewGuid().ToString(), Id = Guid.NewGuid().ToString(), Description = "Download Notifications", Subjects = new string[] { "NotificationId" } };

            try
            {
                Directory.Delete(m_DownloadDirectory, true);
            }
            catch
            {
            }
        }

        private void InvokeProgressUpdate(ItemProgress theProgressItem, int thePercentage, string theProgress)
        {
            theProgressItem.Progress = theProgress;
            theProgressItem.Percentage = thePercentage.ToString();

            DownloadProgressEvent.Invoke(new Payload(DownloadProgressEvent.Id, new PayloadSubject[]
            {
                new PayloadSubject(DownloadProgressEvent.Subjects[0], theProgressItem.Guid),
                new PayloadSubject(DownloadProgressEvent.Subjects[1], thePercentage.ToString()),
                new PayloadSubject(DownloadProgressEvent.Subjects[2], theProgress)
            }));
        }

        private void InvokeNotification(bool theSuccess)
        {

            NotificationEvent.Invoke(new Payload(NotificationEvent.Id, new PayloadSubject[]
            {
                new PayloadSubject(NotificationEvent.Subjects[0], theSuccess ? "0" : "1"),
            }));
        }

        internal void RestartMultiPlug()
        {
            try
            {
                m_MultiPlugActionsAPI.System.Power.Restart();
            }
            catch (SecurityException)
            {
                PermissionsErrorRestart = true;
                PermissionsErrorInstall = false;
                RestartRequired = false;
            }
        }

        internal ItemProgress Queue(string theID, string theVersion)
        {
            if (string.IsNullOrEmpty(theID) || string.IsNullOrEmpty(theVersion))
            {
                return null;
            }

            if (m_Progress.FirstOrDefault(Item => Item.Name == theID && Item.Version == theVersion) != null)
            {
                return null;
            }

            var Result = new ItemProgress(theID, theVersion);

            var ProgressList = new List<ItemProgress>(m_Progress);
            ProgressList.Add(Result);

            m_Progress = ProgressList.ToArray();

            return Result;
        }

        internal void Download(ItemProgress theProgressItem, string theUrlToRegistration)
        {
            if(string.IsNullOrEmpty(theUrlToRegistration))
            {
                InvokeProgressUpdate(theProgressItem, 100, "Error: Registration URL Null");
                return;
            }

            Task.Run(() =>
            {
                Task.Delay(2000).Wait(); // Wait for the User Page to refresh

                InvokeProgressUpdate(theProgressItem, 5, "Fetching Nuget Registration");
                Models.NugetOrg.Registration.Root NugetRegistration = GetNugetRegistration(theUrlToRegistration);

                if( ! CheckedMultiPlugCompatiblilty(theProgressItem, NugetRegistration, m_CurrentMultiPlugVersion) )
                {
                    return;
                }

                InvokeProgressUpdate(theProgressItem, 15, "Fetching Package URL");
                var PackageUrl = GetPackageContentUrl(NugetRegistration);

                string PackageFileName = "";

                try
                {
                    InvokeProgressUpdate(theProgressItem, 20, "Downloading Package");
                    PackageFileName = DownloadPackage(PackageUrl, m_DownloadDirectory);

                }
                catch
                {
                    InvokeProgressUpdate(theProgressItem, 100, "Error: Downloading");
                    //Log?.Invoke(EventLogEntryCodes.UpdateDownloadFailedException, new string[0]);
                    return;
                }

                InvokeProgressUpdate(theProgressItem, 70, "Unzipping Package");
                string HomeDirectory = ProcessNupkgFile(m_DownloadDirectory, PackageFileName, theProgressItem.Guid);

                if (!string.IsNullOrEmpty(HomeDirectory))
                {
                    try
                    {
                        m_MultiPlugActionsAPI.System.Setup.BeginInstallationFromDirectory(HomeDirectory);
                        RestartRequired = true;
                        PermissionsErrorRestart = false;
                        PermissionsErrorInstall = false;
                        InvokeProgressUpdate(theProgressItem, 100, "Complete");
                        InvokeNotification(true);
                    }
                    catch (SecurityException)
                    {
                        PermissionsErrorInstall = true;
                        PermissionsErrorRestart = false;
                        RestartRequired = false;
                        InvokeProgressUpdate(theProgressItem, 100, "Error: Permissions");
                        InvokeNotification(false);
                    }
                }
            });
        }

        public ItemProgress[] Progress
        {
            get { return m_Progress; }
        }

        internal static string GetPackageContentUrl(Models.NugetOrg.Registration.Root theNugetRegistration)
        {
            if (theNugetRegistration.items != null &&
                theNugetRegistration.items.Any() &&
                theNugetRegistration.items.Last().items != null &&
                theNugetRegistration.items.Last().items.Any())
            {
                return theNugetRegistration.items.Last().items.Last().packageContent; // Add checks                  
            }
            else
            {
                return "";
            }
        }

        private bool CheckedMultiPlugCompatiblilty(ItemProgress theProgressItem, Models.NugetOrg.Registration.Root theNugetRegistration, string theCurrentMultiPlugVersion)
        {
            InvokeProgressUpdate(theProgressItem, 9, "Checking Compatibility with MultiPlug version " + theCurrentMultiPlugVersion);

            if (theNugetRegistration.items != null && 
                theNugetRegistration.items.Any() && 
                theNugetRegistration.items.Last().items != null && 
                theNugetRegistration.items.Last().items.Any())
            {
                foreach (var Group in theNugetRegistration.items.Last().items.Last().catalogEntry.dependencyGroups)
                {
                    foreach (var Dependency in Group.dependencies)
                    {
                        if (Dependency.id.Equals("multiplug.core", StringComparison.OrdinalIgnoreCase))
                        {
                            VersionRange VersionRange = VersionRange.Parse(Dependency.range);

                            if( ! VersionRange.Satisfies(new NuGetVersion(theCurrentMultiPlugVersion)) )
                            {
                                InvokeProgressUpdate(theProgressItem, 100, "Error: Requires MultiPlug version " + VersionRange.MinVersion.ToString());
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return true;
        }

        internal static Models.NugetOrg.Registration.Root GetNugetRegistration(string theRegistrationURL)
        {
            string responseFromServer = string.Empty;

            WebRequest request = WebRequest.Create(theRegistrationURL);

            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = response.GetResponseStream();

                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }

            if (responseFromServer != string.Empty)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Models.NugetOrg.Registration.Root>(responseFromServer);
            }
            else
            {
                return null;
            }
        }

        private string DownloadPackage(string theUrl, string theDownloadDirectory)
        {
            Uri uri = new Uri(theUrl);

            string filename = uri.LocalPath.Substring(uri.LocalPath.LastIndexOf("/") + 1);

            if (!Directory.Exists(theDownloadDirectory))
            {
                Directory.CreateDirectory(theDownloadDirectory);
            }

            using (WebClient Client = new WebClient())
            {
                string SaveLocation = Path.Combine(new string[] { theDownloadDirectory, filename });

                Client.DownloadFile(theUrl, SaveLocation);

                return filename;
            }
        }

        private string ProcessNupkgFile(string theDownloadDirectory, string theNupkgFileNameWithExtension, string theGuid)
        {
            string NupkgFileNameWithoutExtension = theNupkgFileNameWithExtension.Replace(".nupkg", string.Empty); // Remove the file extension

            string ExtractPath = Path.Combine(new string[] { theDownloadDirectory, Guid.NewGuid().ToString() }); // Full directory path of the extracted files

            string NupkgFileNameWithExtensionWithPath = Path.Combine(new string[] { theDownloadDirectory, theNupkgFileNameWithExtension }); // Full path to the nupkg file with file extension

            if (UnZipPackage(NupkgFileNameWithExtensionWithPath, ExtractPath))
            {
                string[] NuspecSearch = System.IO.Directory.GetFiles(ExtractPath, "*.nuspec");

                if (NuspecSearch.Length == 0)
                {
                    //Log?.Invoke(EventLogEntryCodes.UpdateFindNuspecFileException, new string[0]);
                    return string.Empty;
                }

                string NuspecFile = NuspecSearch[0].Split(Path.DirectorySeparatorChar).Last();
                string ExtensionName = NuspecFile.Replace(".nuspec", string.Empty);
                string DllFile = NuspecFile.Replace(".nuspec", ".dll");
                string LibDirectory = Path.Combine(new string[] { ExtractPath, "lib" });
                string HomeDirectory = SearchForHome(LibDirectory, DllFile);

                if (HomeDirectory != string.Empty)
                {
                    bool CopyErrored = false;
                    try
                    {
                        CopyFolder(HomeDirectory, Path.Combine(theDownloadDirectory, theGuid, ExtensionName));
                    }
                    catch
                    {
                        CopyErrored = true;
                        //Log?.Invoke(EventLogEntryCodes.UpdateCopyPackageDirectoryException, new string[0]);
                    }

                    if (!CopyErrored)
                    {
                        try
                        {
                            Directory.Delete(ExtractPath, true);
                        }
                        catch
                        {
                            //Log?.Invoke(EventLogEntryCodes.UpdateDeleteExtensionTempDirectoryException, new string[0]);
                        }

                        try
                        {
                            File.Delete(NupkgFileNameWithExtensionWithPath);
                        }
                        catch
                        {
                            //Log?.Invoke(EventLogEntryCodes.UpdateDeleteExtensionNupkgFileException, new string[0]);
                        }
                    }
                }
                else
                {
                    //Log?.Invoke(EventLogEntryCodes.UpdateFindHomeDirectoryException, new string[0]);
                }
            }
            else
            {
                //Log?.Invoke(EventLogEntryCodes.UpdateUnzipPackageException, new string[0]);
            }

            return Path.Combine(theDownloadDirectory, theGuid);
        }
        private bool UnZipPackage(string thePackageUri, string theExtractDirectory)
        {
            if (Directory.Exists(theExtractDirectory))
            {
                try
                {
                    Directory.Delete(theExtractDirectory, true);
                }
                catch (IOException)
                {
                    return false;
                }
            }
            try
            {
                ZipFile.ExtractToDirectory(thePackageUri, theExtractDirectory);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private string SearchForHome(string theStartDir, string theTarget)
        {
            var result = Directory.GetFiles(theStartDir, theTarget, SearchOption.AllDirectories);

            string Returned = "";

            if (result.Any())
            {
                Returned = result[0].TrimEnd(theTarget.ToArray());
            }

            return Returned;
        }

        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

    }
}
