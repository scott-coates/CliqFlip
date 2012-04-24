using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace CliqFlip.Infrastructure.Web
{
    public class PageDetails
    {
        public string SiteName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ShortlinkUrl { get; set; }
        public string OpenGraphUrl { get; set; }
    }
}
