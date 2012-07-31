namespace CliqFlip.Domain.Dtos.UserInterest
{
    public class InterestInCommonDto
    {
        public string Name { get; set; }
        public float Score { get; set; }
        public bool IsExactMatch { get; private set; }

        public InterestInCommonDto(string name, float score, bool isExactMatch)
        {
            Name = name;
            Score = score;
            IsExactMatch = isExactMatch;
        }
    }
}