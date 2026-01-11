using MultiPlug.Base.Http;
using MultiPlug.Ext.Nuget.Models.Settings;
using MultiPlug.Extension.Core.Exchange;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;

namespace MultiPlug.Ext.Nuget.Controllers.Apps.UpdateChecker
{
    public class HomeController : UpdateCheckerApp
    {
        public HomeController()
        {
        }

        public Response Get()
        {
            if(this.Context.QueryString != null && this.Context.QueryString.Any())
            {
                var AssemblyNames = this.Context.QueryString.Select( x => x.Key).ToArray();
                var AssemblyVersions = this.Context.QueryString.Select(x => x.Value).ToArray();

                ResultRow[] ResultRows = new ResultRow[0];
                string[] Urls = new string[0];

                ResultRows = new ResultRow[AssemblyNames.Length];
                Urls = new string[AssemblyNames.Length];

                for (int i = 0; i < AssemblyNames.Length; i++)
                {
                    ResultRows[i] = Core.Instance.NugetClient.Get(AssemblyNames[i], AssemblyNames, AssemblyVersions);

                    if (string.IsNullOrEmpty(ResultRows[i].RegistrationURL))
                    {
                        Urls[i] = string.Empty;
                    }
                    else
                    {
                        Models.NugetOrg.Registration.Root NugetRegistration = Components.Download.DownloadManagerComponent.GetNugetRegistration(ResultRows[i].RegistrationURL);

                        Urls[i] = Components.Download.DownloadManagerComponent.GetPackageContentUrl(NugetRegistration);
                    }
                }

                return new Response
                {
                    Template = Templates.UpdateCheckerHome,
                    Model = new Models.Apps.UpdateChecker.Home
                    {
                        ResultRows = ResultRows,
                        Urls = Urls
                    }
                };
            }
            else
            {
                return new Response
                {
                    Template = Templates.UpdateCheckerHome,
                    Model = new Models.Apps.UpdateChecker.Home
                    {
                        ResultRows = new ResultRow[0],
                        Urls = new string[0]
                    }
                };
            }
        }

        public Response Post(UploadFilePaths theFiles)
        {
            ResultRow[] ResultRows = new ResultRow[0];
            string[] Urls = new string[0];

            if (theFiles.Files.Length > 0)
            {
                try
                {
                    var FileString = System.IO.File.ReadAllText(theFiles.Files[0].Path);

                    var Collection = Recipe.ToObject(FileString);

                    if(!Collection.Extensions.Any())
                    {
                        RecipeItem item = new RecipeItem(FileString);
                        Collection = new RecipeCollection { Extensions = new RecipeItem[] { item } };
                    }

                    var AssemblyNames = Collection.Extensions.Select(Extension => string.IsNullOrEmpty(Extension.Assembly) ? string.Empty : Extension.Assembly).ToArray();
                    var AssemblyVersions = Collection.Extensions.Select(Extension => string.IsNullOrEmpty(Extension.Version) ? string.Empty : Extension.Version).ToArray();

                    ResultRows = new ResultRow[Collection.Extensions.Length];
                    Urls = new string[Collection.Extensions.Length];

                    for ( int i = 0; i < Collection.Extensions.Length; i++)
                    {
                        ResultRows[i] = Core.Instance.NugetClient.Get(Collection.Extensions[i].Assembly, AssemblyNames, AssemblyVersions);

                        if( string.IsNullOrEmpty(ResultRows[i].RegistrationURL))
                        {
                            Urls[i] = string.Empty;
                        }
                        else
                        {
                            Models.NugetOrg.Registration.Root NugetRegistration = Components.Download.DownloadManagerComponent.GetNugetRegistration(ResultRows[i].RegistrationURL);

                            Urls[i] = Components.Download.DownloadManagerComponent.GetPackageContentUrl(NugetRegistration);
                        }
                    }
                }
                catch
                {
                    ResultRows = new ResultRow[0];
                    Urls = new string[0];
                }
            }

            return new Response
            {
                Template = Templates.UpdateCheckerHome,
                Model = new Models.Apps.UpdateChecker.Home
                {
                    ResultRows = ResultRows,
                    Urls = Urls
                }
            };
        }
    }
}
