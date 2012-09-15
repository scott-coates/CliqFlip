namespace CliqFlip.Domain.Entities.UserRoot
{
    public class Interest
    {
        public string InterestName { get; private set; }
        public string CategoryName { get; private set; }

        public Interest(string interestName, string categoryName)
        {
            InterestName = interestName;
            CategoryName = categoryName;
        }
    }
}