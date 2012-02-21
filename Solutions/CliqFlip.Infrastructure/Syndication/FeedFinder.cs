using System.Collections.Generic;
using System.Linq;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using HtmlAgilityPack;

namespace CliqFlip.Infrastructure.Syndication
{
	public class FeedFinder : IFeedFinder
	{
		#region IFeedFinder Members

		public string GetFeedUrl(string html)
		{
			string retVal = null;
			var doc = new HtmlDocument();
			doc.LoadHtml(html);
			HtmlNode root = doc.DocumentNode;
			HtmlNodeCollection linkNodes = root.SelectNodes("//link");
			IEnumerable<HtmlAttribute> typeAttributes = linkNodes
				.SelectMany(x => x.Attributes)
				.Where(x => x.Name != null && x.Name.ToLower() == "type");

			HtmlAttribute feedUrl = typeAttributes
				.FirstOrDefault(x =>
									{
										if (x.Value != null)
										{
											string value = x.Value.ToLower();
											if (value.Contains("rss") || value.Contains("atom"))
											{
												return true;
											}
										}
										return false;
									});

			if (feedUrl != null)
			{
				HtmlNode parent = feedUrl.OwnerNode;
				if (parent != null)
				{
					HtmlAttribute href = parent.Attributes["href"];
					if (href != null)
					{
						retVal = href.Value;
					}
				}
			}

			return retVal;
		}

		#endregion
	}
}