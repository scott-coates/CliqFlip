using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Infrastructure.Web.Interfaces;
using HtmlAgilityPack;
//using Fizzler;
//using Fizzler.Systems.HtmlAgilityPack;
//using Fizzler.Systems.XmlNodeQuery;
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
            //the title could be found in the following places
            //<meta name="title" content="Text were interested in" />
            //<meta property="og:title" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for
            //<title>Text were interested in</title > most comment but I would rather use it as a last resort

            HtmlNode _metaTitle = document.DocumentNode.SelectSingleNode("/html/head/meta[@name='title']") ??
                                    document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:title']");
            if (_metaTitle != null)
            {
                return _metaTitle.GetAttributeValue("content", null);
            }
            else
            {
                return document.DocumentNode.SelectSingleNode("/html/head/title").InnerText.Trim();
            }
        }


        private String GetDescription(HtmlDocument document)
        {
            //the description could be found in the following places
            //<meta name="description" content="Text were interested in" />
            //<meta property="og:description" content=” Text were interested in " /> og = FB's OpenGraph, this is what FB looks for

            HtmlNode metaDescription = document.DocumentNode.SelectSingleNode("/html/head/meta[@name='description']") ??
                                    document.DocumentNode.SelectSingleNode("/html/head/meta[@property='og:description']");
            if (metaDescription != null)
            {
                return metaDescription.GetAttributeValue("content", null);
            }
            return null;
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
