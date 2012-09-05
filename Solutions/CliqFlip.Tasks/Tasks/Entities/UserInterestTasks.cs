using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Tasks.Entities
{
    public class UserInterestTasks : IUserInterestTasks
    {
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IInterestScoreCalculator _interestScoreCalculator;
        private readonly IHighestScoreCalculator _highestScoreCalculator;
        private readonly ICloseInterestLimiter _closeInterestLimiter;
        private readonly IMainCategoryScoreCalculator _mainCategoryScoreCalculator;

        public UserInterestTasks(IUserInterestRepository userInterestRepository,
                                 IInterestScoreCalculator interestScoreCalculator,
                                 IHighestScoreCalculator highestScoreCalculator,
                                 ICloseInterestLimiter closeInterestLimiter,
                                 IMainCategoryScoreCalculator mainCategoryScoreCalculator)
        {
            _userInterestRepository = userInterestRepository;
            _interestScoreCalculator = interestScoreCalculator;
            _highestScoreCalculator = highestScoreCalculator;
            _closeInterestLimiter = closeInterestLimiter;
            _mainCategoryScoreCalculator = mainCategoryScoreCalculator;
        }

        public IList<PopularInterestDto> GetMostPopularInterests()
        {
            return _userInterestRepository.GetMostPopularInterests().ToList();
        }

        public IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user)
        {
            var interestsInCommon = _userInterestRepository.GetInterestsInCommon(viewingUser, user).ToList();
            _interestScoreCalculator.CalculateRelatedInterestScore(interestsInCommon);
            var scoredInterests = interestsInCommon.Select(x => new ScoredInterestInCommonDto(x.Id, x.Score, x.Name, x.IsMainCategory, x.IsExactMatch)).ToList();
            scoredInterests = _highestScoreCalculator.CalculateHighestScores(scoredInterests).Cast<ScoredInterestInCommonDto>().ToList();
            _mainCategoryScoreCalculator.CalculateMainCategoryScores(scoredInterests);
            scoredInterests = _closeInterestLimiter.LimitCloseInterests(scoredInterests).Cast<ScoredInterestInCommonDto>().ToList();

            return scoredInterests
                .Select(x => new InterestInCommonDto(x.Name, x.Score, x.IsExactMatch))
                .ToList();
        }

        public void SaveUserInterest(UserInterest userInterest)
        {
            _userInterestRepository.SaveOrUpdate(userInterest);
        }

        public void DeleteUserInterest(UserInterest userInterest)
        {
            _userInterestRepository.Delete(userInterest);
        }
    }
}