namespace CliqFlip.Domain.Dtos
{
    public class RelatedDistanceInterestDto
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public RelatedDistanceInterestDto(int id, int score)
        {
            Id = id;
            Score = score;
        }
    }
}