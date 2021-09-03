using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Nuget.Controllers.Settings.Search
{
    [Route("search")]
    public class SearchController : SettingsApp
    {
        public Response Get(string q)
        {
            return new Response
            {
                Model = new Models.Settings.Search
                {
                    Query = string.IsNullOrEmpty(q) ? string.Empty : q,
                    Row = Core.Instance.NugetClient.Get(q)

                },
                Template = Templates.SettingsSearch
            };
        }
    }
}
