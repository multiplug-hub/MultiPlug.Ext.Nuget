using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Nuget.Controllers;
using MultiPlug.Ext.Nuget.Properties;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;

namespace MultiPlug.Ext.Nuget
{
    public class NugetExtension : MultiPlugExtension
    {
        public NugetExtension()
        {
            Core.Instance.Init(MultiPlugServices, MultiPlugActions, MultiPlugAPI);
        }

        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate(Templates.SettingsNavigation, Resources.Navigation),
                    new RazorTemplate(Templates.SettingsHome, Resources.Home),
                    new RazorTemplate(Templates.SettingsSearch, Resources.Search),
                    new RazorTemplate(Templates.SettingsDownloads, Resources.Downloads),
                    new RazorTemplate(Templates.SettingsAbout, Resources.About)
                };
            }
        }

        public override Event[] Events
        {
            get
            {
                return Core.Instance.Events;
            }
        }

    }
}
