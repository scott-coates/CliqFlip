namespace CliqFlip.Domain.Dtos.Interest
{
    public class ScoredRelatedInterestDto
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Slug { get; set; }
        public bool IsMainCategory { get; set; }
        public bool ExplicitSearch { get; set; }

        public ScoredRelatedInterestDto(int id, float score, string slug, bool isMainCategory, bool explicitSearch)
        {
            Id = id;
            Score = score;
            Slug = slug;
            IsMainCategory = isMainCategory;
            ExplicitSearch = explicitSearch;
        }
    }
}