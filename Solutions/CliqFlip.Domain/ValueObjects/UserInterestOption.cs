using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ValueObjects
{
	public class UserInterestOption : ValueObject
	{
		public virtual float Passion { get; set; }
		public virtual float XAxis { get; set; }
		public virtual float YAxis { get; set; }
	}
}
