using System;
using System.Net;
using CliqFlip.Infrastructure.Web.Interfaces;
using HtmlAgilityPack;
namespace CliqFlip.Infrastructure.Web
{
    public class PageParsingService : IPageParsingService
    {
        public PageDetails GetDetails(string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);
            return new PageDetails{
                SiteName = GetSiteName(document),
                Title = GetTitle(document),
                Description = GetDescription(document),
                ImageUrl = GetMainImage(document),
                VideoUrl = GetMainVideo(document),
                ShortlinkUrl = GetShortlinkUrl(document),
                OpenGraphUrl = GetOpenGraphUrl(document)
            };
        }

        private String GetTitle(HtmlDocument document)
        {
            String retVal = null;
            //the title could be found in the following places
            //<meta name="title" content="Text were interested in" />
            //<meta property="og:title" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for
            //<title>Text were interested in</title > most common but I would rather use it as a last resort

            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/meta[@name='title']") ??
                                    document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:title']");
            if (metaTag != null)
            {
                retVal = WebUtility.HtmlDecode(metaTag.GetAttributeValue("content", null));
            }
            else
            {
                retVal = WebUtility.HtmlDecode(document.DocumentNode.SelectSingleNode("/html/head/title").InnerText.Trim());
            }
            return retVal;
        }


        private String GetDescription(HtmlDocument document)
        {
            String retVal = null;
            //the description could be found in the following places
            //<meta name="description" content="Text were interested in" />
            //<meta property="og:description" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for

            //the names given to meta tags have insconsistent casings EX.(name="Description" || name="description")
            //so we need to ignore the case by making it lower case but
            //apparently .NET only has XPATH v1 so we can't use lower-case
            //we have to use translate instead to make everything lowercase
            //http://channel9.msdn.com/Forums/TechOff/259602-XPath-Whats-wrong-with-my-query

            HtmlNode metaDescription = document.DocumentNode.SelectSingleNode("/html/head/meta[translate(@name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='description']") ??
                                    document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:description']");

            if (metaDescription != null)
            {
                retVal = WebUtility.HtmlDecode(metaDescription.GetAttributeValue("content", null));
            }
            return retVal;
        }

        private String GetMainImage(HtmlDocument document)
        {
            //the image could be found in the following places
            //<meta property="og:image" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for
            //<link rel='image_src' href='Text were interested in'>

            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:image']");
            HtmlNode linkTag = document.DocumentNode.SelectSingleNode("/html/head/link[@rel='image_src']");

            if (metaTag != null)
            {
                return metaTag.GetAttributeValue("content", null);
            }
            else if (linkTag != null)
            {
                return linkTag.GetAttributeValue("href", null);
            }
            return null;
        }

        private String GetMainVideo(HtmlDocument document)
        {
            //we can set src of an iframe to this url to embed the video
            //the video url could be found in the following places
            //<meta property="og:video" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for

            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:video']");

            if (metaTag != null)
            {
                return metaTag.GetAttributeValue("content", null);
            }
            return null;
        }

        public string GetShortlinkUrl(HtmlDocument document)
        {
            //EX: <link rel="shortlink" href="http://youtu.be/EfS1x5RnZZQ" />
            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/link[@rel='shortlink']");
            if (metaTag != null)
            {
                return metaTag.GetAttributeValue("href", null);
            }
            return null;
        }

        public string GetOpenGraphUrl(HtmlDocument document)
        {
            //<meta property="og:url" content="http://www.youtube.com/watch?v=EfS1x5RnZZQ">
            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:url']");
            if (metaTag != null)
            {
                return metaTag.GetAttributeValue("content", null);
            }
            return null;
        }

        public string GetSiteName(HtmlDocument document)
        {
            //<meta property="og:site_name" content="the content">
            HtmlNode metaTag = document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:site_name']");
            if (metaTag != null)
            {
                return metaTag.GetAttributeValue("content", null);
            }
            return null;
        }
    }
}
