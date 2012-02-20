using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.Syndication.Interfaces
{
	public interface IFeedFinder
	{
		string GetFeedUrl(string siteUrl);
	}
}
