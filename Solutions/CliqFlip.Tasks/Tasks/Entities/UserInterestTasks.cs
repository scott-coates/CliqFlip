using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Tasks.Entities
{
    public class UserInterestTasks : IUserInterestTasks
    {
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IInterestScoreCalculator _interestScoreCalculator;
        private readonly IHighestScoreCalculator _highestScoreCalculator;

        public UserInterestTasks(IUserInterestRepository userInterestRepository, IInterestScoreCalculator interestScoreCalculator, IHighestScoreCalculator highestScoreCalculator)
        {
            _userInterestRepository = userInterestRepository;
            _interestScoreCalculator = interestScoreCalculator;
            _highestScoreCalculator = highestScoreCalculator;
        }

        public IList<PopularInterestDto> GetMostPopularInterests()
        {
            return _userInterestRepository.GetMostPopularInterests().ToList();
        }

        public IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user)
        {
            var interestsInCommon = _userInterestRepository.GetInterestsInCommon(viewingUser, user);
            var scoredInterests = _interestScoreCalculator.CalculateRelatedInterestScore(interestsInCommon.ToList());
            scoredInterests = _highestScoreCalculator.CalculateHighestScores(scoredInterests);

            return scoredInterests
                .Select(x => new InterestInCommonDto { Name = x.Slug, Score = x.Score })
                .Where(x => x.Score >= Constants.CLOSE_INTEREST_THRESHOLD)
                .ToList();
        }
    }
}