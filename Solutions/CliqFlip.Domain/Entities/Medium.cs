using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public abstract class Medium : Entity
	{
		public virtual UserInterest UserInterest { get; set; }
		
		public virtual string Description { get; set; }

		public virtual int? InterestMediumOrder
		{
			get
			{
				int? fieldOrder = null;

				if (UserInterest != null && UserInterest.Media.Contains(this))
				{
					fieldOrder = UserInterest.Media.ToList().IndexOf(this) + 1; //why not
				}

				return fieldOrder;
			}
		}
	}
}