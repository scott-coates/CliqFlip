using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Messaging.Commands.User;
using CommonDomain.Persistence;
using Facebook;
using Magnum;
using MassTransit;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.CommandHandlers.User
{
    public class CreateNewUserCommandHander : Consumes<CreateNewUserCommand>.All
    {
        private readonly IRepository _repository;

        public CreateNewUserCommandHander(IRepository repository)
        {
            _repository = repository;
        }

        public void Consume(CreateNewUserCommand message)
        {
            var client = new FacebookClient(message.FacebookAccessToken);

            dynamic result = client.Get("me", new { fields = "id,likes,location,email" });

            string facebookId = result.id;

            var likes = (JsonArray)result.likes["data"];

            string location = result.location.name;
            string email = result.email;

            var likeNames = likes
                .Cast<dynamic>()
                .Select(x => x.name)
                .Cast<string>()
                .Distinct();

            var user = new Domain.Entities.UserRoot.User(CombGuid.Generate(), facebookId, location, email, likeNames);

            _repository.Save(user, CombGuid.Generate());
        }
    }
}