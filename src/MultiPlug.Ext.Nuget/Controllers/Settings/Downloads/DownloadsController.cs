using System;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Exchange;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Nuget.Controllers.Settings.DownloadQueue
{
    [Route("downloads")]
    public class DownloadsController : SettingsApp
    {
        public Response Get(string id, string v)
        {
            var Progress = Core.Instance.DownloadManager.Queue(id, v);

            if( Progress != null)
            {
                var Result = Core.Instance.NugetClient.Get(id);
                Core.Instance.DownloadManager.Download(Progress, Result.RegistrationURL);
            }

            return new Response
            {
                Subscriptions = new Subscription[]
                {
                    new Subscription { Guid = "ProgressEventId", Id = Core.Instance.DownloadManager.DownloadProgressEvent.Id },
                    new Subscription { Guid = "NotificationEventId", Id = Core.Instance.DownloadManager.NotificationEvent.Id },
                },

                Model = new Models.Settings.DownloadQueue
                {
                    Progress = Core.Instance.DownloadManager.Progress,
                    PermissionsErrorInstall = Core.Instance.DownloadManager.PermissionsErrorInstall,
                    PermissionsErrorRestart = Core.Instance.DownloadManager.PermissionsErrorRestart,
                    RestartRequired = Core.Instance.DownloadManager.RestartRequired
                },
                Template = Templates.SettingsDownloads,
                StatusCode = Context.Request.AbsoluteUri.Contains("?") ? System.Net.HttpStatusCode.Moved : System.Net.HttpStatusCode.OK,
                Location = Context.Request.AbsoluteUri.Contains("?") ? new Uri(Context.Request.AbsoluteUri.Split('?')[0]) : Context.Request
            };
        }

        public Response Post()
        {
            Core.Instance.DownloadManager.RestartMultiPlug();

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
