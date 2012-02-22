﻿using System;
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
		public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, String linkText, String actionName, String controllerName)
		{
			var tag = new TagBuilder("li");

			if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
			{
				tag.AddCssClass("selected");
			}

			tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToString();

			return MvcHtmlString.Create(tag.ToString());
		}

        public static MvcHtmlString DisplayYouTube(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
            if (!String.IsNullOrWhiteSpace(htmlHelper.ViewData.Model.YouTubeUsername))
            {
                var div = new TagBuilder("div");
                div.Attributes.Add("id", "youtube-container");
                return new MvcHtmlString(div.ToString());
            }
            return htmlHelper.Partial("_NotShared", "This user is not sharing their YouTube videos.");
        }

        public static MvcHtmlString DisplayTwitter(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
			//TODO: Do we need microsoft.web.helpers.dll anymore?

            var username = htmlHelper.ViewData.Model.TwitterUsername;
            if (!String.IsNullOrWhiteSpace(username))
            {
                var html = Twitter.Profile(username,
                                width: 372,
                                backgroundShellColor: "transparent",
                                tweetsColor: "black",
                                tweetsBackgroundColor: "transparent",
                                tweetsLinksColor: "#008FAE",
                                shellColor: "black").ToHtmlString();
                return new MvcHtmlString(html);
            }
            return htmlHelper.Partial("_NotShared", "This user is not sharing their twitter feed.");
        }

        public static MvcHtmlString DisplayBlogFeed(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
            if (!String.IsNullOrWhiteSpace(htmlHelper.ViewData.Model.WebsiteFeedUrl))
            {
                var div = new TagBuilder("div");
                div.Attributes.Add("id", "blog-container");
                return MvcHtmlString.Create(div.ToString());
            }
            return htmlHelper.Partial("_NotShared", "This user is not sharing their blog feed.");
        }

        public static MvcHtmlString DisplayFacebook(this HtmlHelper<UserProfileViewModel> htmlHelper)
        {
            if (!String.IsNullOrWhiteSpace(htmlHelper.ViewData.Model.FacebookUsername))
            {
                return htmlHelper.Partial("_FacebookLink", htmlHelper.ViewData.Model.FacebookUsername);
            }
            return htmlHelper.Partial("_NotShared", "This user is not sharing their facebook page.");
        }
	}
}