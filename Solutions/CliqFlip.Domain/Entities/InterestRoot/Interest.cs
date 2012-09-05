using System;
using System.ComponentModel.DataAnnotations;
using CommonDomain.Core;

namespace CliqFlip.Domain.Entities.InterestRoot
{
    public class Interest : AggregateBase
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Slug { get; set; }

        public bool IsMainCategory { get; set; }
        public DateTime CreateDate { get; set; }
    }
}