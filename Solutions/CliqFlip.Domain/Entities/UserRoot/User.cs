using System;
using System.Collections.Generic;
using CliqFlip.Messaging.Events.User;
using CommonDomain.Core;

namespace CliqFlip.Domain.Entities.UserRoot
{
    public class User : AggregateBase
    {
        public string Email { get; private set; }
        public Location Location { get; private set; }
        public string Username { get; private set; }
        public IEnumerable<string> Interests { get; private set; }

        public User(Guid id, string username, Location location, string email, IEnumerable<string> interests)
        {
            RaiseEvent(new UserCreatedEvent(id, username, email, interests, location.MajorLocationId, location.LocationName, location.Latitude, location.Longitude));
        }

        private void Apply(UserCreatedEvent @event)
        {
            Id = @event.Id;
            Location = new Location(@event.MajorLocationId, @event.LocationName, @event.Latitude, @event.Longitude);
            Username = @event.Username;
            Interests = @event.Interests;
            Email = @event.Email;
        }
    }
}
