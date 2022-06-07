using MultiPlug.Base;
using MultiPlug.Base.Exchange;
using MultiPlug.Base.Exchange.API;
using MultiPlug.Ext.Nuget.Components.Download;
using MultiPlug.Ext.Nuget.Components.NugetClient;
using MultiPlug.Extension.Core;

namespace MultiPlug.Ext.Nuget
{
    internal class Core : MultiPlugBase
    {
        private static Core m_Instance = null;

        public Event[] Events { get; private set; }

        public NugetClientComponent NugetClient { get; set; }

        public DownloadManagerComponent DownloadManager { get; set; }

        public static Core Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Core();
                }
                return m_Instance;
            }
        }

        private Core()
        {
        }




        internal void Init(IMultiPlugServices theMultiPlugServices, IMultiPlugActions theMultiPlugActions, IMultiPlugAPI theMultiPlugAPI)
        {
            NugetClient  = new NugetClientComponent(theMultiPlugActions, theMultiPlugAPI);
            DownloadManager = new DownloadManagerComponent(theMultiPlugActions, theMultiPlugAPI.Environment.Version);
            Events = new Event[] { DownloadManager.DownloadProgressEvent, DownloadManager.NotificationEvent };
        }
    }
}
