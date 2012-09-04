using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Tasks.Commands.User;
using Facebook;
using MassTransit;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.CommandHandlers.User
{
    public class CreateNewUserCommandHander : Consumes<CreateNewUserCommand>.All
    {
        private readonly IUserTasks _userTasks;

        public CreateNewUserCommandHander(IUserTasks userTasks)
        {
            _userTasks = userTasks;
        }

        public void Consume(CreateNewUserCommand message)
        {
            using (var tx = NHibernateSession.Current.Transaction)
            {
                tx.Begin();
                var client = new FacebookClient(message.FacebookUserId);

                dynamic result = client.Get("me", new { fields = "id,likes,location" });

                var likes = (JsonArray)result.likes["data"];

                string location = result.location.name;

                var likeNames = likes
                    .Cast<dynamic>()
                    .Select(x => x.name)
                    .Cast<string>();

                _userTasks.Create(result.id, location, likeNames);

                tx.Commit();
            }
        }
    }
}