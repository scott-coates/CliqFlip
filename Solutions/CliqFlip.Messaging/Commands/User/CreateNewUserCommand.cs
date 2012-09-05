namespace CliqFlip.Messaging.Commands.User
{
    public class CreateNewUserCommand
    {
        public string FacebookAccessToken { get; private set; }

        public CreateNewUserCommand(string facebookAccessToken)
        {
            FacebookAccessToken = facebookAccessToken;
        }
    }
}