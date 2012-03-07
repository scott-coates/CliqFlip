using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Image : Entity
	{
		private ImageData _data;

		public virtual ImageData Data
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _data ?? new ImageData(null, null, null, null); }
			set { _data = value; }
		}

		public virtual UserInterest UserInterest { get; set; }
		public virtual int InterestImageOrder
		{
			get
			{
				int fieldOrder = 0;

				if (UserInterest != null && UserInterest.Images.Contains(this))
				{
					fieldOrder = UserInterest.Images.ToList().IndexOf(this) + 1; //why not
				}

				return fieldOrder;
			}
		}
	}
}