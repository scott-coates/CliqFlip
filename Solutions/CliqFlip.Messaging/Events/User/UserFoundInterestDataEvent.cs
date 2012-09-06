using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserFoundInterestDataEvent
    {
        public string Username { get; private set; }

        public UserFoundInterestDataEvent(string username)
        {
            Username = username;
        }
    }
}