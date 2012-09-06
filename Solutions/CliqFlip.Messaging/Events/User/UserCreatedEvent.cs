using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserCreatedEvent
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<string> Interests { get; private set; }
        public Guid MajorLocationId { get; private set; }
        public string LocationName { get; private set; }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public UserCreatedEvent(Guid id, string username, string email, IEnumerable<string> interests, Guid majorLocationId, string locationName, float latitude, float longitude)
        {
            Id = id;
            Username = username;
            Email = email;
            Interests = interests;
            MajorLocationId = majorLocationId;
            LocationName = locationName;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}