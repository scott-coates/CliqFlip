namespace CliqFlip.Messaging.Commands.User
{
    public class CreateNewUserCommand
    {
        public string FacebookAccessToken { get; set; }

        public CreateNewUserCommand(string facebookAccessToken)
        {
            FacebookAccessToken = facebookAccessToken;
        }
    }
}