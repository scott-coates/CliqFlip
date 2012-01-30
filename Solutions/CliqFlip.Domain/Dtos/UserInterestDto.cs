namespace CliqFlip.Domain.Dtos
{
	public class UserInterestDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public string RelatedTo { get; set; }
		public int? Sociality { get; set; }
		public float? Passion { get; set; }
		public float? XAxis { get; set; }
		public float? YAxis { get; set; }

		public UserInterestDto(int id, string name, string slug)
		{
			Id = id;
			Name = name;
			Slug = slug;
		}

		public UserInterestDto(int id, string name, string relatedTo, int? sociality)
		{
			Id = id;
			Name = name;
			RelatedTo = relatedTo;
			Sociality = sociality;
		}

		public UserInterestDto(int id, string name, string slug, string relatedTo, int? sociality, float? passion, float? xAxis, float? yAxis)
		{
			Id = id;
			Name = name;
			Slug = slug;
			RelatedTo = relatedTo;
			Sociality = sociality;
			Passion = passion;
			XAxis = xAxis;
			YAxis = yAxis;
		}
	}
}