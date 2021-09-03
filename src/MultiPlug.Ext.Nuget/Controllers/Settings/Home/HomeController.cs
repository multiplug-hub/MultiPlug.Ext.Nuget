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
                    Rows = Core.Instance.NugetClient.Get()
                },
                Template = Templates.SettingsHome
            };
        }
    }
}
