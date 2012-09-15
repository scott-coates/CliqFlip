using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Messaging.Events.User;
using CliqFlip.Messaging.Events.User.Dtos;
using CommonDomain.Core;

namespace CliqFlip.Domain.Entities.UserRoot
{
    public class User : AggregateBase
    {
        public string Email { get; private set; }
        public Location Location { get; private set; }
        public Name Name { get; private set; }
        public ProfileImage ProfileImage { get; private set; }
        public string Username { get; private set; }
        public IEnumerable<Interest> Interests { get; private set; }

        public User(Guid id, string username, Location location, Name name, ProfileImage profileImage, string email, IEnumerable<Interest> interests)
        {
            RaiseEvent(new UserCreatedEvent(id, username, email, interests.Select(x => new UserCreatedInterestDto(x.InterestName, x.CategoryName)), location.MajorLocationId, location.LocationName, location.Latitude, location.Longitude, name.FirstName, name.LastName, profileImage.SquareImageUrl, profileImage.LargeImageUrl));
        }

        private void Apply(UserCreatedEvent @event)
        {
            Id = @event.Id;
            Location = new Location(@event.MajorLocationId, @event.LocationName, @event.Latitude, @event.Longitude);
            Username = @event.Username;
            Interests = @event.Interests.Select(x => new Interest(x.InterestName, x.CategoryName));
            Email = @event.Email;
            Name = new Name(@event.FirstName, @event.LastName);
            ProfileImage = new ProfileImage(@event.SquareImageUrl, @event.LargeImageUrl);
        }
    }
}
