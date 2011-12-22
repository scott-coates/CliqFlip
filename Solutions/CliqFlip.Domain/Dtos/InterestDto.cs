using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Dtos
{
	public class Interest : Entity
	{
		public virtual Interest ParentInterest { get; set; }

		public virtual string Name { get; set; }
	}
}