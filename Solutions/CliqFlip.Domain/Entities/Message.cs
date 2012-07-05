using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Message : Entity
    {
        public virtual string Text { get; set; }
        public virtual DateTime SendDate { get; set; }
        public virtual User Sender { get; set; }
        public virtual Conversation Conversation { get; set; }
        
        public Message() //todo: make private ctor
        {
        }

        public Message(User user,  String text)
        {
            SendDate = DateTime.UtcNow;
            Text = text;
            Sender = user;
        }
    }
}