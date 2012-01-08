using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Interest : Entity
    {
        public virtual Subject Subject { get; set; }
        public virtual int? SocialityPoints { get; set; }
    }
}
