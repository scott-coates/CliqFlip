using System;
using System.ComponentModel;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Notification : Entity
	{
		public virtual string Message { get; set; }
		public virtual DateTime CreateDate { get; set; }
	}
}