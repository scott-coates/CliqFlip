using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserExaminedEvent
    {
        public string Username { get; private set; }

        public UserExaminedEvent(string username)
        {
            Username = username;
        }
    }
}