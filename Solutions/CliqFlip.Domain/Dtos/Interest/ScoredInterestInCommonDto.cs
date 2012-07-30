using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Domain.Dtos.Interest
{
    public class ScoredInterestInCommonDto : IScoredInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ScoredInterestInCommonDto(int id, float score, string name, bool isMainCategory)
        {
            Id = id;
            Score = score;
            Name = name;
            IsMainCategory = isMainCategory;
        }

        #region IScoredRelatedInterestDto Members

        public float Score { get; set; }

        public bool IsMainCategory { get; set; }

        #endregion
    }
}