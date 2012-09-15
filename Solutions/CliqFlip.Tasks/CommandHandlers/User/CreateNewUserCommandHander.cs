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

            dynamic result = client.Get("me", new { fields = "id,likes,location,email,first_name,last_name,picture.type(square)" });

            string facebookId = result.id;

            var likes = (JsonArray)result.likes["data"];

            string locationName = result.location.name;

            var location = _locationService.GetLocation(locationName);
            var majorLocation = _locationService.GetNearestMajorCity(location.Latitude, location.Longitude);
            string firstName = result.first_name;
            string lastName = result.first_name;

            dynamic largePicResult = client.Get("me", new { fields = "picture.type(large)" });
            string squarePic = result.picture;
            string largePic = largePicResult.picture;

            string email = result.email;

            var likeNames = likes
                .Cast<dynamic>()
                .Select(x => new Interest(x.name, x.category));

            var user = new Domain.Entities.UserRoot.User(CombGuid.Generate(), facebookId, new Location(new Guid(majorLocation.Guid), locationName, location.Latitude, location.Longitude), new Name(firstName, lastName), new ProfileImage(squarePic, largePic), email, likeNames);

            _repository.Save(user, CombGuid.Generate());
        }
    }
}