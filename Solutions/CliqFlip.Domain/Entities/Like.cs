using System;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Like : Entity
    {
        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public virtual DateTime CreateDate { get; set; }
    }
}