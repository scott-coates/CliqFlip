using System;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
	public abstract class Medium : Entity
	{
		public virtual DateTime CreateDate { get; set; }
	}
}