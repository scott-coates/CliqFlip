namespace CliqFlip.Domain.Dtos
{
	public class InterestDto
	{
		public InterestDto(int id, string name)
		{
			Id = id;
			Name = name;
		}

        public InterestDto(int id, string name, string relatedTo, int? sociality)
        {
            Id = id;
            Name = name;
            RelatedTo = relatedTo;
            Sociality = sociality;
        }

		public int Id { get; set; }
		public string Name { get; set; }
        public string RelatedTo { get; set; }
        public int? Sociality { get; set; }
	}
}