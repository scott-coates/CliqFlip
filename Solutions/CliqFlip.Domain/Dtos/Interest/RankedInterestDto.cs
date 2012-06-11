namespace CliqFlip.Domain.Dtos.Interest
{
	public class RankedInterestDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int Count { get; set; }

		public RankedInterestDto(int id, string name, string slug, int count)
		{
			Id = id;
			Name = name;
			Slug = slug;
			Count = count;
		}
	}
}