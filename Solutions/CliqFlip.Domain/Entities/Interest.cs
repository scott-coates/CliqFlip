using System;
using System.ComponentModel.DataAnnotations;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Extensions;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Interest : Entity
    {
        public virtual Interest ParentInterest { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual string Slug { get; set; }

        public virtual bool IsMainCategory { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public Interest()
        {
        }

        public Interest(string name)
        {
            Name = name;
            Slug = name.Slugify();
        }

        public Interest(int id, string name)
        {
            Id = id;
            Name = name;
            Slug = name.Slugify();
        }
    }
}