using System.Collections.Generic;

namespace CliqFlip.Domain.Dtos.Interest.Interfaces
{
    public interface IWeightedInterestDto
    {
        List<float> Weight { get; set; }
        float Score { get; set; }
    }
}