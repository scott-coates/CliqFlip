using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
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

		public IList<InterestFeedItemDto> GetMediaByInterests(IList<Interest> interests)
		{
			//get user interests
			//get sibling and parent interests
			var interestAndParentInterests = interests
				.Concat(interests.Select(x => x.ParentInterest))
				.Where(x => x != null)
				.Distinct()
				.ToList();

			var userIntersts = _userInterestRepository.GetUserInterestsByInterestTypes(interestAndParentInterests).ToList();

			//get all media with any matching interests order by date desc limit by 100
			var recentMedia = userIntersts.SelectMany(x => x.Media).OrderByDescending(x => x.CreateDate).Take(100);

			/*
			 * my thinking is take the last 100 because a user probably won't read past that..
			 * if we only grab the first 10 then there is the problem that 11th might be extremely relevent 
			 * but shows up much later on cause it's
			 * slightly older
			 */

			return recentMedia.Select(medium =>
			{
				int rank = 0;

				//we know the medium has a userInterest tied to it
				if (interests.Contains(medium.UserInterest.Interest))
					rank = 10;
				else if (medium.UserInterest.Interest.ParentInterest != null && interestAndParentInterests.Contains(medium.UserInterest.Interest.ParentInterest))
					rank = 2;
				else if (interestAndParentInterests.Contains(medium.UserInterest.Interest))
					rank = 1;

				int daysSinceMediumCreated = (DateTime.UtcNow - medium.CreateDate).Days;

				rank -= daysSinceMediumCreated;

				return new { Rank = rank, FeedItem = new InterestFeedItemDto(medium) };

			}).OrderByDescending(x => x.Rank).Select(x => x.FeedItem).ToList();
		}

		public void SaveOrUpdate(UserInterest interest)
		{
			_userInterestRepository.SaveOrUpdate(interest);
		}

		public void Delete(UserInterest interest)
		{
			_userInterestRepository.Delete(interest);
		}
	}
}