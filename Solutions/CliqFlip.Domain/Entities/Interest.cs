using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Interest : Entity
	{
        private int p;
        private string p_2;

		public virtual Interest ParentInterest { get; set; }

		public virtual string Name { get; set; }

        public Interest()
        { 
        }
        public Interest(string name)
        {
            this.Name = name;
        }

        public Interest(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}