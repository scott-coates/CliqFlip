using CliqFlip.Domain.Common;
using CliqFlip.Domain.Extensions;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Interest : Entity
	{
		public virtual Interest ParentInterest { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string Slug { get; set; }
        public virtual bool IsMainCategory { get; set; }

		public Interest()
		{
		}

		public Interest(string name)
		{
			Name = name;
			Slug = name.Slugify();
		}

		public Interest(int id, string name)
		{
			Id = id;
			Name = name;
			Slug = name.Slugify();
		}
	}
}