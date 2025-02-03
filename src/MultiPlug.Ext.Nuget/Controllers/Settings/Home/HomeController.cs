using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Nuget.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Settings.Home
                {
                    Rows = Core.Instance.NugetClient.Get(Core.Instance.DownloadManager.Progress.Select(p => p.Name).ToArray()),
                    PermissionsErrorInstall = Core.Instance.DownloadManager.PermissionsErrorInstall,
                    PermissionsErrorRestart = Core.Instance.DownloadManager.PermissionsErrorRestart,
                    RestartRequired = Core.Instance.DownloadManager.RestartRequired
                },
                Template = Templates.SettingsHome
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
