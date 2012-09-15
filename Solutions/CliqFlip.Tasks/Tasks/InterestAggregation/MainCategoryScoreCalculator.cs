using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Common;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class MainCategoryScoreCalculator : IMainCategoryScoreCalculator
    {
        public void CalculateMainCategoryScores<T>(IList<T> scoredInterests) where T : class, IScoredInterestDto
        {
            if (scoredInterests == null) throw new ArgumentNullException("scoredInterests");

            foreach (var scoredRelatedInterestDto in scoredInterests.Where(x => x.IsMainCategory))
            {
                scoredRelatedInterestDto.Score *= Constants.MAIN_CATEGORY_INTEREST_MULTIPLIER;
            }
        }
    }
}