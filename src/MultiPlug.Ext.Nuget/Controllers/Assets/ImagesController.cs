using System;
using System.Drawing;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Nuget.Properties;

namespace MultiPlug.Ext.Nuget.Controllers.Assets
{
    [Route("images/*")]
    public class ImagesController : AssetsEndpoint
    {
        public Response Get(string theName)
        {
            if (string.Equals(theName, "nuget.png", StringComparison.OrdinalIgnoreCase))
            {
                ImageConverter converter = new ImageConverter();
                return new Response { RawBytes = (byte[])converter.ConvertTo(Resources.NugetLogo, typeof(byte[])), MediaType = "image/png" };
            }
            else
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
        }
    }
}
