using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Participant : Entity
    {
        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }
        public virtual bool HasUnreadMessages { get; set; }
        public virtual bool IsActive { get; set; }
    }
}