namespace CliqFlip.Domain.Dtos
{
    public class UserAddInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RelatedTo { get; set; }

        public UserAddInterestDto(int id, string name, int? relatedTo)
        {
            Id = id;
            Name = name;
            RelatedTo = relatedTo;
        }

        public UserAddInterestDto(int interestId)
        {
            Id = interestId;
        }
    }
}