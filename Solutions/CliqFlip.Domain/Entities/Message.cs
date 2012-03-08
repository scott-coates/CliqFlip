using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Message : Entity
    {
        public String Text { get; set; }
        public DateTime SendDate { get; set; }
        public User Sender { get; set; }

        public Message(User user,  String text)
        {
            SendDate = DateTime.UtcNow;
        }
    }
}