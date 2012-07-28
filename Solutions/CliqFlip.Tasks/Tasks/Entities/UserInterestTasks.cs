using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Tasks.Entities
{
    public class UserInterestTasks : IUserInterestTasks
    {
        private readonly IUserInterestRepository _userInterestRepository;

        public UserInterestTasks(IUserInterestRepository userInterestRepository)
        {
            _userInterestRepository = userInterestRepository;
        }


        public IList<PopularInterestDto> GetMostPopularInterests()
        {
            return _userInterestRepository.GetMostPopularInterests().ToList();
        }

        public IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user)
        {
            return _userInterestRepository.GetInterestsInCommon(viewingUser, user)
                .Select(x => new InterestInCommonDto { Name = x.Slug, Score = 0f }).ToList();
        }
    }
}