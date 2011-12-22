namespace CliqFlip.Domain.Dtos
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