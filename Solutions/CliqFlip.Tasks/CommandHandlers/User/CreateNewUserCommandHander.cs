using System.Linq;
using CliqFlip.Tasks.Commands.User;
using CliqFlip.Tasks.Tasks.Entities;
using Facebook;
using MassTransit;

namespace CliqFlip.Tasks.CommandHandlers.User
{
    public class CreateNewUserCommandHander : Consumes<CreateNewUserCommand>.All
    {
        private readonly UserTasks _userTasks;

        public CreateNewUserCommandHander(UserTasks userTasks)
        {
            _userTasks = userTasks;
        }

        public void Consume(CreateNewUserCommand message)
        {
            var client = new FacebookClient(message.FacebookUserId);

            dynamic result = client.Get("me", new { fields = "id,likes,location" });

            var likes = (JsonArray)result.likes["data"];

            string location = result.location.name;

            var likeNames = likes
                .Cast<dynamic>()
                .Select(x => x.name)
                .Cast<string>();

            var user = _userTasks.Create(result.id, location, likeNames);

            _userTasks.Login(user, true);
        }
    }
}