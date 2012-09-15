namespace CliqFlip.Messaging.Events.User.Dtos
{
    public class UserCreatedInterestDto
    {
        public string InterestName { get; private set; }
        public string CategoryName { get; private set; }

        public UserCreatedInterestDto(string interestName, string categoryName)
        {
            InterestName = interestName;
            CategoryName = categoryName;
        }
    }
}