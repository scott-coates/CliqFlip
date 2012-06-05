using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Infrastructure.Neo.Entities
{
    public class NeoInterest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int SqlId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Slug { get; set; }

        public string Description { get; set; }
        public bool IsMainCategory { get; set; }
    }
}