using System;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
    public class Like : Entity
    {
        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public virtual DateTime CreateDate { get; set; }
    }
}