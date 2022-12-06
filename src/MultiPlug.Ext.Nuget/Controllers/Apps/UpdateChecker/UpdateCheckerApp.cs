using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.Nuget.Controllers.Apps.UpdateChecker
{
    [Hidden]
    [HttpEndpointType(HttpEndpointType.App)]
    [ViewAs(ViewAs.FullScreen)]
    [Name("Update Checker")]

    public class UpdateCheckerApp : Controller
    {
    }
}
