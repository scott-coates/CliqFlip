using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ValueObjects
{
	public class UserWebsite : ValueObject
	{
		//http://davybrion.com/blog/2009/03/implementing-a-value-object-with-nhibernate/
		private readonly string _feedUrl;
		private readonly string _siteUrl;

		public string SiteUrl
		{
			get { return _siteUrl; }
		}

		public string FeedUrl
		{
			get { return _feedUrl; }
		}

		private UserWebsite()
		{
			// the default constructor is only here for NH (private is sufficient, it doesn't need to be public)
		}

		public UserWebsite(string siteUrl, string feedUrl)
		{
			_siteUrl = siteUrl;
			_feedUrl = feedUrl;
		}
	}
}