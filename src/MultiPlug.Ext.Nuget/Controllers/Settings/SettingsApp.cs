using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.Nuget.Controllers.Settings
{
    [Name("Nuget Gallery")]
    [ViewAs(ViewAs.Partial)]
    [HttpEndpointType(HttpEndpointType.Settings)]
    public class SettingsApp : Controller
    {
    }
}
