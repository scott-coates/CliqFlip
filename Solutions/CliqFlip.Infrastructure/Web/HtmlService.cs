using System.Net;
using CliqFlip.Infrastructure.Web.Interfaces;

namespace CliqFlip.Infrastructure.Web
{
	public class HtmlService : IHtmlService
	{
		#region IHtmlService Members

		public string GetHtmlFromUrl(string url)
		{
			string retVal;
			using (var wc = new WebClient())
			{
				retVal = wc.DownloadString(url);
			}

			return retVal;
		}

		#endregion
	}
}