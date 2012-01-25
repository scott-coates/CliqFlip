using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class UserInterest : Entity
    {
        public virtual User User { get; set; }
        public virtual Interest Interest { get; set; }
        public virtual int? SocialityPoints { get; set; }
    }
}
