namespace CliqFlip.Domain.Dtos.UserInterest
{
    public class InterestInCommonDto
    {
        public string Name { get; private set; }
        public float Score { get; private set; }
        public bool IsExactMatch { get; private set; }
        public int InterestId { get; private set; }

        public InterestInCommonDto(string name, float score, bool isExactMatch, int interestId)
        {
            Name = name;
            Score = score;
            IsExactMatch = isExactMatch;
            InterestId = interestId;
        }
    }
}