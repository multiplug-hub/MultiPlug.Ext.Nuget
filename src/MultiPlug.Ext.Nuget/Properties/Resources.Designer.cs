﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiPlug.Ext.Nuget.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MultiPlug.Ext.Nuget.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///    &lt;div class=&quot;row-fluid&quot;&gt;
        ///        &lt;div class=&quot;box&quot;&gt;
        ///            &lt;div class=&quot;span2&quot;&gt;
        ///                &lt;a style=&quot;line-height: 52px;&quot; href=&quot;#&quot;&gt;&lt;img alt=&quot;Nuget Logo&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/nuget.png&quot;&gt;&lt;/a&gt;
        ///            &lt;/div&gt;
        ///            &lt;div class=&quot;span8&quot;&gt;
        ///                &lt;p style=&quot;font-size:26px; line-height: 54px; text-align: center; margin: 0px;&quot;&gt;Nuget Gallery&lt;/p&gt;
        ///            &lt;/div&gt;
        ///        &lt;/div&gt;
        ///    &lt;/div&gt;
        ///
        /// </summary>
        internal static string About {
            get {
                return ResourceManager.GetString("About", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///    &lt;div class=&quot;row-fluid&quot;&gt;
        ///        &lt;div class=&quot;box&quot;&gt;
        ///            &lt;div class=&quot;span4&quot;&gt;
        ///                &lt;a style=&quot;line-height: 52px;&quot; href=&quot;#&quot;&gt;&lt;img alt=&quot;Nuget Logo&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/nuget.png&quot;&gt;&lt;/a&gt;
        ///            &lt;/div&gt;
        ///            &lt;div class=&quot;span4&quot;&gt;
        ///                &lt;p style=&quot;font-size:26px; line-height: 54px; text-align: center; margin: 0px;&quot;&gt;Nuget Gallery&lt;/p&gt;
        ///            &lt;/div&gt;
        ///        &lt;/div&gt;
        ///    &lt;/div&gt;
        ///
        /// </summary>
        internal static string Downloads {
            get {
                return ResourceManager.GetString("Downloads", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///@functions {
        ///    public string theAction(string theHomeUrl, string theItemName, bool canInstall, bool canUpdate)
        ///    {
        ///        if(canInstall)
        ///        {
        ///            return &quot;&lt;a href=\&quot;&quot; + theHomeUrl + &quot;search/?q=&quot; + theItemName + &quot;\&quot;&gt;Install&lt;/a&gt;&quot;;
        ///        }
        ///        else if (canUpdate)
        ///        {
        ///            return &quot;&lt;a href=\&quot;&quot; + theHomeUrl + &quot;search/?q=&quot; + theItemName + &quot;\&quot;&gt;Update&lt;/a&gt;&quot;;
        ///        }
        ///        else
        ///        {
        ///            return string.Empty;
        ///        } [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Home {
            get {
                return ResourceManager.GetString("Home", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///@functions {
        ///    public string NavLocationIsHome()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsSearch()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home + &quot;search/&quot; ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsAbout()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home + &quot;about/&quot; ? &quot;active&quot;  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Navigation {
            get {
                return ResourceManager.GetString("Navigation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap NugetLogo {
            get {
                object obj = ResourceManager.GetObject("NugetLogo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///@functions {
        ///    public string theAction(bool canInstall, bool canUpdate)
        ///    {
        ///        if (canInstall)
        ///        {
        ///            return &quot;&lt;button class=\&quot;btn\&quot; type=\&quot;button\&quot;&gt;Install&lt;/button&gt;&quot;;
        ///        }
        ///        else if (canUpdate)
        ///        {
        ///            return &quot;&lt;button class=\&quot;btn\&quot; type=\&quot;button\&quot;&gt;Update&lt;/button&gt;&quot;;
        ///        }
        ///        else
        ///        {
        ///            return string.Empty;
        ///        }
        ///    }
        ///}
        ///
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Search {
            get {
                return ResourceManager.GetString("Search", resourceCulture);
            }
        }
    }
}