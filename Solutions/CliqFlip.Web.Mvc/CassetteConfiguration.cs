using System.IO;
using System.Text.RegularExpressions;
using Cassette.Configuration;
using Cassette.HtmlTemplates;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace CliqFlip.Web.Mvc
{
    /// <summary>
    /// Configures the Cassette asset modules for the web application.
    /// </summary>
    public class CassetteConfiguration : ICassetteConfiguration
    {
        public void Configure(BundleCollection bundles, CassetteSettings settings)
        {
            //client-side logic 
            bundles.Add<ScriptBundle>("Content/app");

            //css, less, sass
            bundles.AddPerSubDirectory<StylesheetBundle>("Content/assets/styles");

            //this dir is for plugins, site js, etc...ignore cliqflip stuff tho
            bundles.AddPerSubDirectory<ScriptBundle>("Content/assets/js", new FileSearch { Exclude = new Regex("js\\cliqflip") });

            //put all of cliqflip in one file
            bundles.Add<ScriptBundle>("Content/assets/js/cliqflip", new FileSearch { SearchOption = SearchOption.AllDirectories, Exclude = new Regex("errorHandler.js|bookmarklet") });
            bundles.Add<ScriptBundle>("Content/assets/js/cliqflip/main/errorHandler.js", bundle => bundle.PageLocation = "head");

            //NuGet
            bundles.Add<ScriptBundle>("Scripts", new FileSearch { Exclude = new Regex("modernizr|_references.js|-vsdoc\\.js$") });

            //only modernizr
            bundles.Add<ScriptBundle>("Scripts/modernizr-2.5.3.js", bundle => bundle.PageLocation = "head");

            //templates
            bundles.Add<HtmlTemplateBundle>("Content/assets/templates", bundle => bundle.Processor = new HoganPipeline());
        }
    }
}