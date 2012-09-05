using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
	public class UserInterest : Entity
	{
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
	}
}