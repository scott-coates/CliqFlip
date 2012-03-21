using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Location : Entity
	{
		public virtual LocationData Data { get; set; }
		public virtual MajorLocation MajorLocation { get; set; }
	}
}