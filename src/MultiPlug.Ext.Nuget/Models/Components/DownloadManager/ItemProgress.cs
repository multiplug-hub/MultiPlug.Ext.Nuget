
namespace MultiPlug.Ext.Nuget.Models.Components.DownloadManager
{
    public class ItemProgress
    {
        public ItemProgress()
        {
        }

        public ItemProgress(string theName, string theVersion)
        {
            Name = theName;
            Version = theVersion;
            Progress = string.Empty;
            Percentage = "0";        }

        public ItemProgress(string theName, string theVersion, string theProgress)
        {
            Name = theName;
            Version = theVersion;
            Progress = theProgress;
        }

        public string Guid { get; private set; } = System.Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Version { get; set; }

        public string Percentage { get; set; }

        public string Progress { get; set; }
    }
}
