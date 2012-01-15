using SharpArch.Domain.DomainModel;
using CliqFlip.Domain.Common;

namespace CliqFlip.Domain.Entities
{
	public class Subject : Entity
	{
		public virtual Subject ParentSubject { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string SystemAlias { get; set; }

        public Subject()
        { 
        }
        public Subject(string name)
        {
            this.Name = name;
            this.SystemAlias = name.Slugify();
        }

        public Subject(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.SystemAlias = name.Slugify();
        }
    }
}