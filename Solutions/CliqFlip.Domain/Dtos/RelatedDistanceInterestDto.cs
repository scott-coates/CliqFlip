namespace CliqFlip.Domain.Dtos
{
    public class RelatedDistanceInterestDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Slug { get; set; }

        public RelatedDistanceInterestDto(int id, int score, string slug)
        {
            Id = id;
            Score = score;
            Slug = slug;
        }
    }
}