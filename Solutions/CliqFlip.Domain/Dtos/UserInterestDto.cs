namespace CliqFlip.Domain.Dtos
{
	public class UserInterestDto
	{
		public UserInterestDto(int id, string name,string slug)
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

		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
        public string RelatedTo { get; set; }
        public int? Sociality { get; set; }
	}
}