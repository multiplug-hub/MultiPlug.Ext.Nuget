using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Nuget.Models.Components.DownloadManager
{
    class DownloadManagerProperties : MultiPlugBase
    {
        public Event DownloadProgressEvent { get; set; }

        public Event NotificationEvent { get; set; }
    }
}
