using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserRequestedSuggestedUsersEvent
    {
        public string Username { get; private set; }

        public UserRequestedSuggestedUsersEvent(string username)
        {
            Username = username;
        }
    }
}