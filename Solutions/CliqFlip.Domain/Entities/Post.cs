using System;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Post : Entity
	{
		public virtual UserInterest UserInterest { get; set; }

		public virtual Medium Medium { get; set; }
		
		public virtual string Description { get; set; }

		public virtual DateTime CreateDate { get; set; }

		public virtual int? InterestPostOrder
		{
			get
			{
				int? fieldOrder = null;

				if (UserInterest != null && UserInterest.Posts.Contains(this))
				{
                    fieldOrder = UserInterest.Posts.ToList().IndexOf(this) + 1; //why not
				}

				return fieldOrder;
			}
		}
	}
}