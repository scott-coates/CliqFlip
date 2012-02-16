using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CliqFlip.Web.Mvc.Extensions.Request;
using CliqFlip.Web.Mvc.ViewModels.User;
using Microsoft.Web.Helpers;

namespace CliqFlip.Web.Mvc.Extensions.Html
{
	public static class HtmlExtensions
	{
		public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, String linkText, String actionName, String controllerName, object routeValues = null)
		{
			var tag = new TagBuilder("li");

			if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
			{
				tag.AddCssClass("selected");
			}

			tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null).ToString();

			return MvcHtmlString.Create(tag.ToString());
		}

        public static MvcHtmlString DisplayYouTube(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
            var html = string.Empty;
            if (!String.IsNullOrWhiteSpace(htmlHelper.ViewData.Model.YouTubeUsername))
            {
                var div = new TagBuilder("div");
                div.Attributes.Add("id", "youtube-container");
                html = div.ToString();
            }
            else
            {
                var strong = new TagBuilder("strong");
                strong.SetInnerText("This user is not sharing their youtube videos.");
                strong.AddCssClass("not-shared");
                html = strong.ToString();
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString DisplayTwitter(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
            var html = string.Empty;
            var username = htmlHelper.ViewData.Model.TwitterUsername;
            if (!String.IsNullOrWhiteSpace(username))
            {
                html = Twitter.Profile(username,
                                width: 372,
                                backgroundShellColor: "transparent",
                                tweetsColor: "black",
                                tweetsBackgroundColor: "transparent",
                                tweetsLinksColor: "#008FAE",
                                shellColor: "black").ToHtmlString();
            }
            else
            {
                var strong = new TagBuilder("strong");
                strong.SetInnerText("This user is not sharing their Twitter feed.");
                strong.AddCssClass("not-shared");
                html = strong.ToString();
            }
            return MvcHtmlString.Create(html);
        }

	}

}