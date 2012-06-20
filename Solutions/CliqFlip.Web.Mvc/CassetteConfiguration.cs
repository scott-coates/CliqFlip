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
            //css, less, sass
            bundles.AddPerSubDirectory<StylesheetBundle>("Content/assets/styles");

            //this dir is for plugins, site js, etc...ignore cliqflip stuff tho
            bundles.AddPerSubDirectory<ScriptBundle>("Content/assets/js", new FileSearch { Exclude = new Regex("js\\cliqflip") });

            //put all of cliqflip in one file
            bundles.Add<ScriptBundle>("Content/assets/js/cliqflip", new FileSearch { Exclude = new Regex("js\\cliqflip\\error-handler.js") });
            bundles.Add<ScriptBundle>("Content/assets/js/cliqflip/error-handler.js", bundle => bundle.PageLocation = "head");

            //NuGet
            bundles.Add<ScriptBundle>("Scripts", new FileSearch { Exclude = new Regex("modernizr") });

            //only modernizr
            //http://stackoverflow.com/questions/1153856/string-negation-using-regular-expressions
            bundles.Add<ScriptBundle>("Scripts", new FileSearch { Exclude = new Regex("^(?!.*modernizr).*$") }, bundle => bundle.PageLocation = "head");

            //templates
            bundles.Add<HtmlTemplateBundle>("Content/assets/templates", bundle => bundle.Processor = new HoganPipeline());
        }
    }
}