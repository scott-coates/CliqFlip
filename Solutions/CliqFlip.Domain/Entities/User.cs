using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;
using nh = Iesi.Collections.Generic;

namespace CliqFlip.Domain.Entities
{
	public class User : Entity
	{
		private readonly Iesi.Collections.Generic.ISet<UserInterest> _interests;
		private UserImage _profileImage;
		private UserWebsite _userWebsite;

		public virtual IEnumerable<UserInterest> Interests
		{
			get { return new List<UserInterest>(_interests).AsReadOnly(); }
		}

		public virtual UserImage ProfileImage
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _profileImage ?? new UserImage(null, null, null, null); }
			set { _profileImage = value; }
		}

		public virtual UserWebsite UserWebsite
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _userWebsite ?? new UserWebsite(null, null); }
			set { _userWebsite = value; }
		}

		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string Salt { get; set; }
		public virtual string Bio { get; set; }
		public virtual string Headline { get; set; }
		public virtual string TwitterUsername { get; set; }
		public virtual string YouTubeUsername { get; set; }
		public virtual DateTime CreateDate { get; set; }
		public virtual DateTime LastActivity { get; set; }

		public User()
		{
			_interests = new HashedSet<UserInterest>();
		}

		public User(string username, string email, string password, string salt)
			: this()
		{
			Username = username;
			Email = email;
			Password = password;
			Salt = salt;
		}

		public virtual void AddInterest(Interest interest, int? socialityPoints)
		{
			var userInterest = new UserInterest
								{
									User = this,
									Interest = interest,
									SocialityPoints = socialityPoints
								};

			_interests.Add(userInterest);
		}

		public virtual void UpdateInterest(int userInterestId, UserInterestOption userInterestOption)
		{
			UserInterest userInterest = _interests.First(x => x.Id == userInterestId);
			userInterest.Options = userInterestOption;
		}

		public virtual void UpdateHeadline(string headline)
		{
			Headline = !string.IsNullOrWhiteSpace(headline) ? headline.Trim() : null;
		}

		public virtual void UpdateBio(string bio)
		{
			Bio = !string.IsNullOrWhiteSpace(bio) ? bio.Trim() : null;
		}

		public virtual void UpdateTwitterUsername(string twitterUsername)
		{
			TwitterUsername = !string.IsNullOrWhiteSpace(twitterUsername) ? twitterUsername.Trim() : null;
		}

		public virtual void UpdateYouTubeUsername(string youTubeUsername)
		{
			//white space before/after in the username causes problems when making a request to the youtube api
			YouTubeUsername = !string.IsNullOrWhiteSpace(youTubeUsername) ? youTubeUsername.Trim() : null;
		}

		public virtual void UpdateProfileImage(string originalFilename, string thumbFilename, string mediumFilename, string fullFilename)
		{
			_profileImage = new UserImage(originalFilename, thumbFilename, mediumFilename, fullFilename);
		}

		public virtual void UpdateWebsite(string siteUrl, string feedUrl)
		{
			_userWebsite = new UserWebsite(siteUrl, feedUrl);
		}

		public virtual void UpdateLastActivity()
		{
			LastActivity = DateTime.UtcNow;
		}

		public virtual void UpdateCreateDate()
		{
			CreateDate = DateTime.UtcNow;
		}
	}
}