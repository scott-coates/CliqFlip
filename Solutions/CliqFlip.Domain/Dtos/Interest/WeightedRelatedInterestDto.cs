using System.Collections.Generic;

namespace CliqFlip.Domain.Dtos.Interest
{
    public class WeightedRelatedInterestDto
    {
        public int Id { get; set; }
        public List<float> Weight { get; set; }
        public string Slug { get; set; }
        public bool IsMainCategory { get; set; }
        public bool ExplicitSearch { get; set; }

        public WeightedRelatedInterestDto(int id, List<float> weight, string slug, bool isMainCategory)
        {
            Id = id;
            Weight = weight;
            Slug = slug;
            IsMainCategory = isMainCategory;
        }
    }
}