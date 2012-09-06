using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserRequestedPeopleEvent
    {
        public string Username { get; private set; }

        public UserRequestedPeopleEvent(string username)
        {
            Username = username;
        }
    }
}