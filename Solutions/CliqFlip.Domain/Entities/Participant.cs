using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Participant : Entity
    {
        public Conversation Conversation { get; set; }
        public User User { get; set; }
        public bool HasUnreadMessages { get; set; }
    }
}