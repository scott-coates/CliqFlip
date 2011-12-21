namespace CliqFlip.Domain
{
	using SharpArch.Domain.DomainModel;

	public class Interest : Entity
	{
		public virtual Interest ParentInterest { get; set; }

		public virtual string Name { get; set; }
	}
}