using System;
using System.Collections.Generic;

namespace CliqFlip.Messaging.Events.User
{
    public class UserFoundGeneralDataEvent
    {
        public string Username { get; private set; }

        public UserFoundGeneralDataEvent(string username)
        {
            Username = username;
        }
    }
}