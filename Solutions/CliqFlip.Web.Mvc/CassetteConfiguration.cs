using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cassette;
using Cassette.HtmlTemplates;
using Cassette.Scripts;
using Cassette.Stylesheets;
using Cassette.TinyIoC;
using Castle.Core.Configuration;

namespace CliqFlip.Web.Mvc
{
    /// <summary>
    /// Configures the Cassette asset modules for the web application.
    /// </summary>
    public class CassetteConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            //client-side logic 
            bundles.Add<ScriptBundle>("Content/app");

            //css, less, sass
            bundles.AddPerSubDirectory<StylesheetBundle>("Content/assets/styles", new FileSearch { Exclude = new Regex("^_.*scss") });

            //this dir is for plugins, site js, etc...ignore cliqflip stuff tho
            bundles.AddPerSubDirectory<ScriptBundle>("Content/assets/scripts", new FileSearch { Exclude = new Regex("js\\cliqflip") });

            //put all of cliqflip in one file
            bundles.Add<ScriptBundle>("Content/assets/scripts/cliqflip", new FileSearch { SearchOption = SearchOption.AllDirectories, Exclude = new Regex("errorHandler.js|bookmarklet") });
            bundles.Add<ScriptBundle>("Content/assets/scripts/cliqflip/main/errorHandler.js", bundle => bundle.PageLocation = "head");

            //NuGet
            bundles.Add<ScriptBundle>("Scripts", new FileSearch { Exclude = new Regex("modernizr|_references.js|-vsdoc\\.js$") });

            //only modernizr
            bundles.Add<ScriptBundle>("Scripts/modernizr-2.5.3.js", bundle => bundle.PageLocation = "head");

            //templates
            bundles.Add<HtmlTemplateBundle>("Content/assets/templates");

            //Pusher
            bundles.AddUrlWithAlias("//js.pusher.com/1.12/pusher.min.js", "Pusher");
            
            //Angular
            bundles.AddUrlWithAlias("//ajax.googleapis.com/ajax/libs/angularjs/1.0.2/angular.min.js", "Angular");
        }
    }
}