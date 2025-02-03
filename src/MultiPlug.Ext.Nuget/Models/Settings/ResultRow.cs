
namespace MultiPlug.Ext.Nuget.Models.Settings
{
    public class ResultRow
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ProjectURL { get; set; }
        public string Author { get; set; }
        public string LatestVersion { get; set; }
        public string CurrentVersion { get; set; }
        public string RegistrationURL { get; set; }
        public bool Install { get; set; }
        public bool Update { get; set; }
        public bool Restart { get; set; }
    }
}
