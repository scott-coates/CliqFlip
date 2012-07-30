namespace CliqFlip.Domain.Dtos.Interest.Interfaces
{
    public interface IScoredInterestDto
    {
        int Id { get; set; }
        float Score { get; set; }
        bool IsMainCategory { get; set; }
    }
}