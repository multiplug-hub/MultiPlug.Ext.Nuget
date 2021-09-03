﻿using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Exchange;
using MultiPlug.Extension.Core;
using MultiPlug.Ext.Nuget.Models.Settings;
using MultiPlug.Ext.Nuget.Models.Components.NugetClient;

namespace MultiPlug.Ext.Nuget.Components.NugetClient
{
    class NugetClientComponent : NugetClientProperties
    {
        private IMultiPlugActions m_MultiPlugActionsAPI;
        private IMultiPlugAPI m_MultiPlugAPI;

        public NugetClientComponent(IMultiPlugActions theMultiPlugActions, IMultiPlugAPI theMultiPlugAPI)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            m_MultiPlugActionsAPI = theMultiPlugActions;
            m_MultiPlugAPI = theMultiPlugAPI;
        }

        private string HttpFetch(string Query)
        {
            string responseFromServer = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create("https://api-v2v3search-0.nuget.org/query?q=" + Query + "&prerelease=true&take=50");

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
            }
            catch (WebException e)
            {
                // Log?.Invoke(EventLogEntryCodes.UpdatePackageManagerDataFetchException, new[] { e.Message });
            }

            return responseFromServer;
        }

        internal ResultRow Get(string Query)
        {
            if( string.IsNullOrEmpty(Query))
            {
                return new ResultRow { Name = string.Empty };
            }

            string responseFromServer = HttpFetch(Query);

            if (responseFromServer != string.Empty)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.NugetOrg.Search.Root>(responseFromServer);
                if(model.data.Any())
                {
                    var data = model.data.First();
                
                    var ResultRow = new ResultRow
                    {
                        Name = data.id,
                        LatestVersion = data.version,
                        Install = true,
                        Title = data.title,
                        Description = data.description,
                        ProjectURL = data.projectUrl,
                        Author = data.authors.FirstOrDefault(),
                        Summary = data.summary,
                        RegistrationURL = data.registration
                    };

                    foreach (Base.Exchange.IExtension Extension in m_MultiPlugAPI.Extensions)
                    {

                        if(ResultRow.Name.Equals(Extension.Meta.Assembly, StringComparison.OrdinalIgnoreCase))
                        {
                            ResultRow.Install = false;
                            ResultRow.CurrentVersion = Extension.Meta.FileVersion;

                            Version Latest = new Version(ResultRow.LatestVersion);
                            Version Current = new Version(ResultRow.CurrentVersion);

                            if (Latest.CompareTo(Current) == 1)
                            {
                                ResultRow.Update = true;
                            }

                            break;
                        }
                    }

                    return ResultRow;
                }
            }

            return new ResultRow { Name = string.Empty };
        }

        internal ResultRow[] Get()
        {

            // var Result = new List<KeyValuePair<string, ResponseProperties>>();

            string responseFromServer = HttpFetch("MultiPlug.Ext.");



            var list = new List<ResultRow>();


            if (responseFromServer != string.Empty)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.NugetOrg.Search.Root>(responseFromServer);

                foreach (var data in model.data)
                {
                    if( data.id.StartsWith("multiplug.ext.", StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(new ResultRow { Name = data.id, LatestVersion = data.version, Install = true });
                    }
                }
            }

            foreach(Base.Exchange.IExtension Extension in m_MultiPlugAPI.Extensions )
            {

                ResultRow Search = list.FirstOrDefault(NugetResult => NugetResult.Name.Equals(Extension.Meta.Assembly, StringComparison.OrdinalIgnoreCase));

                if(Search != null)
                {
                    Search.Install = false;
                    Search.CurrentVersion = Extension.Meta.FileVersion;

                    Version Latest = new Version(Search.LatestVersion);
                    Version Current = new Version(Search.CurrentVersion);

                    if( Latest.CompareTo(Current) == 1)
                    {
                        Search.Update = true;
                    }
                }
                else
                {
                    list.Add(new ResultRow { Name = Extension.Meta.Assembly, CurrentVersion = Extension.Meta.FileVersion });
                }
            }

            return list.ToArray();
        }

    }
}
