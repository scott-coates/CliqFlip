using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Domain.Dtos.Interest
{
    public class ScoredInterestInCommonDto : IScoredInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExactMatch { get; private set; }

        public ScoredInterestInCommonDto(int id, float score, string name, bool isMainCategory, bool isExactMatch)
        {
            Id = id;
            Score = score;
            Name = name;
            IsMainCategory = isMainCategory;
            IsExactMatch = isExactMatch;
        }

        #region IScoredRelatedInterestDto Members

        public float Score { get; set; }

        public bool IsMainCategory { get; set; }

        #endregion
    }
}