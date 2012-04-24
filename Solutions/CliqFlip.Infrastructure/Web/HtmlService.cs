using System;
using System.Net;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Web.Interfaces;

namespace CliqFlip.Infrastructure.Web
{
	public class HtmlService : IHtmlService
	{
		#region IHtmlService Members

        //TODO: download string fails if 'http://' is missing
		public string GetHtmlFromUrl(string url)
		{
			string retVal;

			using (var wc = new WebClient())
			{
				try
				{
					retVal = wc.DownloadString(url);
				}
				catch (WebException)
				{
					throw new RulesException("website", "Invalid website");
				}
			}

			return retVal;
		}

		#endregion
	}
}