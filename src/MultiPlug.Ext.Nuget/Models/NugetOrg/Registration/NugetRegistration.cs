using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlug.Ext.Nuget.Models.NugetOrg.Registration
{
    public class Dependency
    {
        [JsonProperty("@id")]
        public string id2 { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
        public string id { get; set; }
        public string range { get; set; }
        public string registration { get; set; }
    }

    public class DependencyGroup
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
        public IList<Dependency> dependencies { get; set; }
    }

    public class CatalogEntry
    {
        [JsonProperty("@id")]
        public string id2 { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
        public string authors { get; set; }
        public IList<DependencyGroup> dependencyGroups { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string licenseUrl { get; set; }
        public bool listed { get; set; }
        public string minClientVersion { get; set; }
        public string packageContent { get; set; }
        public string projectUrl { get; set; }
        public DateTime published { get; set; }
        public bool requireLicenseAcceptance { get; set; }
        public string summary { get; set; }
        public IList<string> tags { get; set; }
        public string title { get; set; }
        public string version { get; set; }
    }

    public class Item2
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
        public string commitId { get; set; }
        public DateTime commitTimeStamp { get; set; }
        public CatalogEntry catalogEntry { get; set; }
        public string packageContent { get; set; }
        public string registration { get; set; }
    }

    public class Item
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
        public string commitId { get; set; }
        public DateTime commitTimeStamp { get; set; }
        public int count { get; set; }
        public IList<Item2> items { get; set; }
        public string parent { get; set; }
        public string lower { get; set; }
        public string upper { get; set; }
    }

    public class Items
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@container")]
        public string @container { get; set; }
    }

    public class CommitTimeStamp
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
    }

    public class CommitId
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
    }

    public class Count
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
    }

    public class Parent
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public string @type { get; set; }
    }

    public class Tags
    {
        [JsonProperty("@container")]
        public string @container { get; set; }
        [JsonProperty("@id")]
        public string @id { get; set; }
    }

    public class PackageTargetFrameworks
    {
        [JsonProperty("@container")]
        public string @container { get; set; }
        [JsonProperty("@id")]
        public string @id { get; set; }
    }

    public class DependencyGroups
    {
        [JsonProperty("@container")]
        public string @container { get; set; }
        [JsonProperty("@id")]
        public string @id { get; set; }
    }

    public class Dependencies
    {
        [JsonProperty("@container")]
        public string @container { get; set; }
        [JsonProperty("@id")]
        public string @id { get; set; }

        public List<Dependency> dependencies { get; set; }
    }

    public class PackageContent
    {
        [JsonProperty("@type")]
        public string @type { get; set; }
    }

    public class Published
    {
        [JsonProperty("@type")]
        public string @type { get; set; }
    }

    public class Registration
    {
        [JsonProperty("@type")]
        public string @type { get; set; }
    }

    public class Context
    {
        [JsonProperty("@vocab")]
        public string @vocab { get; set; }
        public string catalog { get; set; }
        public string xsd { get; set; }
        public Items items { get; set; }
        public CommitTimeStamp commitTimeStamp { get; set; }
        public CommitId commitId { get; set; }
        public Count count { get; set; }
        public Parent parent { get; set; }
        public Tags tags { get; set; }
        public PackageTargetFrameworks packageTargetFrameworks { get; set; }
        public DependencyGroups dependencyGroups { get; set; }
        public Dependencies dependencies { get; set; }
        public PackageContent packageContent { get; set; }
        public Published published { get; set; }
        public Registration registration { get; set; }
    }

    public class Root
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        [JsonProperty("@type")]
        public IList<string> @type { get; set; }
        public string commitId { get; set; }
        public DateTime commitTimeStamp { get; set; }
        public int count { get; set; }
        public IList<Item> items { get; set; }
        [JsonProperty("@context")]
        public Context @context { get; set; }
    }
}
