using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class InterestDto
	{
		public InterestDto(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int Id { get; set; }

		public string Name { get; set; }
	}
}