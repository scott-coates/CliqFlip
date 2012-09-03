namespace CliqFlip.Tasks.Commands.User
{
    public class CreateNewUserCommand
    {
        public string FacebookUserId { get; set; }

        public CreateNewUserCommand(string facebookUserId)
        {
            FacebookUserId = facebookUserId;
        }
    }
}