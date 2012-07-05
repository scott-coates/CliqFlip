using System;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public abstract class Medium : Entity
	{
		public virtual DateTime CreateDate { get; set; }
	}
}