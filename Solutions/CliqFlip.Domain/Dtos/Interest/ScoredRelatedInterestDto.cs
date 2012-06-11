namespace CliqFlip.Domain.Dtos.Interest
{
    public class ScoredRelatedInterestDto
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Slug { get; set; }

        public ScoredRelatedInterestDto(int id, float score, string slug)
        {
            Id = id;
            Score = score;
            Slug = slug;
        }
    }
}