using CliqFlip.Domain.Common;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Interest : Entity
	{
		public virtual Interest ParentInterest { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string SystemAlias { get; set; }

		public Interest()
		{
		}

		public Interest(string name)
		{
			Name = name;
			SystemAlias = name.Slugify();
		}

		public Interest(int id, string name)
		{
			Id = id;
			Name = name;
			SystemAlias = name.Slugify();
		}
	}
}