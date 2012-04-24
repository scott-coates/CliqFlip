using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace CliqFlip.Infrastructure.Web.Interfaces
{
    public interface IPageParsingService
    {
        PageDetails GetDetails(string content);
    }
}