using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Domain.Dtos.Interest
{
    public class WeightedInterestInCommonDto : IWeightedInterestDto
    {
        public int Id { get; set; }
        public List<float> Weight { get; set; }
        public float Score { get; set; }
        public string Name { get; set; }
        public bool IsMainCategory { get; set; }

        public WeightedInterestInCommonDto(int id, List<float> weight, string name, bool isMainCategory)
        {
            Id = id;
            Weight = weight;
            Name = name;
            IsMainCategory = isMainCategory;
        }
    }
}