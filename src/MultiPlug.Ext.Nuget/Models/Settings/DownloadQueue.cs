
using MultiPlug.Ext.Nuget.Models.Components.DownloadManager;

namespace MultiPlug.Ext.Nuget.Models.Settings
{
    public class DownloadQueue
    {
        public bool PermissionsErrorInstall { get; set; }
        public bool PermissionsErrorRestart { get; set; }
        public bool RestartRequired { get; set; }
        public string ProgressEventId { get; set; }
        public ItemProgress[] Progress { get; set; }
        public string NotificationEventId { get; set; }

    }
}
