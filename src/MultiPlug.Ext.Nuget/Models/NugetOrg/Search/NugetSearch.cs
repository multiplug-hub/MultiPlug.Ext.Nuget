using System.Collections.Generic;
using Newtonsoft.Json;

namespace MultiPlug.Ext.Nuget.Models.NugetOrg.Search
{
    //public class Context
    //{
    //    public string @vocab { get; set; }
    //    public string @base { get; set; }
    //}

    //public class Version
    //{
    //    public string version { get; set; }
    //    public int downloads { get; set; }
    //    public string @id { get; set; }
    //}

    public class Datum
    {
        //[JsonProperty("@id")]
        //public string @idurl { get; set; }
        //public string @type { get; set; }
        public string registration { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        //public string iconUrl { get; set; }
        public string projectUrl { get; set; }
        //public IList<string> tags { get; set; }
        public IList<string> authors { get; set; }
        //public ulong totalDownloads { get; set; }
        //public bool verified { get; set; }
        //public IList<Version> versions { get; set; }
    }

    public class Root
    {
        //public Context @context { get; set; }
        //public ulong totalHits { get; set; }
        //public DateTime lastReopen { get; set; }
        //public string index { get; set; }
        public IList<Datum> data { get; set; }
    }
}
