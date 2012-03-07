using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Image : Entity
	{
		private UserImage _data;

		public virtual UserImage Data
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _data ?? new UserImage(null, null, null, null); }
			set { _data = value; }
		}

		public virtual UserInterest UserInterest { get; set; }
	}
}