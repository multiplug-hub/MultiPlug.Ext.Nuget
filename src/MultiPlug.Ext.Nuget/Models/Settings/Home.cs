
namespace MultiPlug.Ext.Nuget.Models.Settings
{
    public class Home
    {
        public bool PermissionsErrorInstall { get; set; }
        public bool PermissionsErrorRestart { get; set; }
        public bool RestartRequired { get; set; }
        public ResultRow[] Rows { get; set; }
    }
}
