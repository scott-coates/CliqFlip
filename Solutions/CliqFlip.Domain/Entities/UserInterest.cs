using System.Collections.Generic;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class UserInterest : Entity
	{
		private readonly Iesi.Collections.Generic.ISet<Medium> _media;
		private UserInterestOption _options;

		public virtual User User { get; set; }
		public virtual Interest Interest { get; set; }
		public virtual int? SocialityPoints { get; set; }

		public virtual UserInterestOption Options
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _options ?? new UserInterestOption(null, null, null); }
			set { _options = value; }
		}

		public virtual IEnumerable<Medium> Media
		{
			get { return new List<Medium>(_media).AsReadOnly(); }
		}

		public UserInterest()
		{
			_media = new HashedSet<Medium>();
		}

		//TODO: consider law of demeter violation - should we be working with user class instead of directly with userInterest??
		//http://msdn.microsoft.com/en-us/magazine/cc947917.aspx#id0070040 - i think we can skip the law of demeter since we're working
		//directly with user intersts
		public virtual void AddMedium(Medium medium)
		{
			medium.UserInterest = this;

			_media.Add(medium);
		}

		public virtual void MakeMediumDefault(Medium medium)
		{
			var temp = new List<Medium>(_media);
			_media.Clear();
			_media.Add(medium);
			temp.Remove(medium);
			_media.AddAll(temp);
		}

		public virtual void RemoveInterestMedium(Medium medium)
		{
			_media.Remove(medium);
		}
	}
}