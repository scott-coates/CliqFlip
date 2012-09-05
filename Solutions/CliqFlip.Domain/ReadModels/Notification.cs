using System;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
	public class Notification : Entity
	{
		public virtual string Message { get; set; }
		public virtual DateTime CreateDate { get; set; }
	}
}