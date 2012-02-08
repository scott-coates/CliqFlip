using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;
using nh = Iesi.Collections.Generic;

namespace CliqFlip.Domain.Entities
{
	public class User : Entity
	{
		private readonly nh.ISet<UserInterest> _interests;

		public virtual IEnumerable<UserInterest> Interests
		{
			get { return new List<UserInterest>(_interests).AsReadOnly(); }
		}

		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string Salt { get; set; }
		public virtual string Bio { get; set; }
		public virtual string Headline { get; set; }

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

		public virtual void UpdateInterests(IEnumerable<UserInterestDto> userInterests)
		{
			foreach (var userInterestDto in userInterests)
			{
				var userInterest = _interests.First(x => x.Id == userInterestDto.Id);
				userInterest.Options = new UserInterestOption(userInterestDto.Passion, userInterestDto.XAxis, userInterestDto.YAxis);
			}
		}

        public virtual void UpdateHeadline(string headline)
        {
            this.Headline = headline;
        }

        public virtual void UpdateBio(string bio)
        {
            this.Bio = bio;
        }
    }
}