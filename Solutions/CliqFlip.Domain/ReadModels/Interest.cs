using System;
using System.ComponentModel.DataAnnotations;
using CliqFlip.Common.Extensions;
using CliqFlip.Domain.Extensions;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
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

        public virtual void SetSlug()
        {
            var name = Name;
            if(ParentInterest != null)
            {
                name += " " + ParentInterest.Name;
            }

            Slug = name.Slugify();
        }
    }
}