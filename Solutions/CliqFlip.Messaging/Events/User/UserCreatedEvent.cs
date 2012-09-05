using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserCreatedEvent
    {
        private readonly Guid _id;
        private readonly string _username;
        private readonly IEnumerable<string> _interests;
        private readonly string _location;

        public string Username
        {
            get { return _username; }
        }

        public IEnumerable<string> Interests
        {
            get { return _interests; }
        }

        public string Location
        {
            get { return _location; }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public UserCreatedEvent(Guid id, string username, string location, IEnumerable<string> interests)
        {
            _id = id;
            _username = username;
            _location = location;
            _interests = interests;
        }
    }
}