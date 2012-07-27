using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
    public class UserInterestTasks : IUserInterestTasks
    {
        private readonly IUserInterestRepository _userInterestRepository;

        public UserInterestTasks(IUserInterestRepository userInterestRepository)
        {
            _userInterestRepository = userInterestRepository;
        }

        public IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user)
        {
            return _userInterestRepository.GetInterestsInCommon(viewingUser, user)
                .Select(x => new InterestInCommonDto { Name = x.Slug, Score = 0f }).ToList();
        }
    }
}