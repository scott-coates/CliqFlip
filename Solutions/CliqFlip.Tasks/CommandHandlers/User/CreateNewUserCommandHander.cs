using System;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Entities.UserRoot;
using CliqFlip.Infrastructure.Location.Interfaces;
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
        private readonly ILocationService _locationService;

        public CreateNewUserCommandHander(IRepository repository, ILocationService locationService)
        {
            _repository = repository;
            _locationService = locationService;
        }

        public void Consume(CreateNewUserCommand message)
        {
            var client = new FacebookClient(message.FacebookAccessToken);

            dynamic result = client.Get("me", new { fields = "id,likes,location,email" });

            string facebookId = result.id;

            var likes = (JsonArray)result.likes["data"];

            string locationName = result.location.name;

            var location = _locationService.GetLocation(locationName);
            var majorLocation = _locationService.GetNearestMajorCity(location.Latitude, location.Longitude);

            string email = result.email;

            var likeNames = likes
                .Cast<dynamic>()
                .Select(x => x.name)
                .Cast<string>()
                .Distinct();

            var user = new Domain.Entities.UserRoot.User(CombGuid.Generate(), facebookId, new Location(new Guid(majorLocation.Guid), locationName, location.Latitude, location.Longitude), email, likeNames);

            _repository.Save(user, CombGuid.Generate());
        }
    }
}