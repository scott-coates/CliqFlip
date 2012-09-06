using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserCreatedEvent
    {
        public string Username { get; private set; }
        public IEnumerable<string> Interests { get; private set; }
        public string Location { get; private set; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }

        public UserCreatedEvent(Guid id, string username, string location, string email, IEnumerable<string> interests)
        {
            Id = id;
            Username = username;
            Location = location;
            Email = email;
            Interests = interests;
        }
    }
}