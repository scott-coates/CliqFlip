using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Search;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
	public class InterestTasks : IInterestTasks
	{
		private readonly IInterestRepository _interestRepository;
		private readonly IUserInterestRepository _userInterestRepository;

		public InterestTasks(IInterestRepository interestRepository, IUserInterestRepository userInterestRepository)
		{
			_interestRepository = interestRepository;
			_userInterestRepository = userInterestRepository;
		}

		#region IInterestTasks Members

		public IList<InterestKeywordDto> GetMatchingKeywords(string input)
		{
			var retVal = new List<InterestKeywordDto>();

			var subjs = _interestRepository.GetMatchingKeywords(input).ToList(); //tolist - prevent deferred ex many times

			if (subjs.Any())
			{
				retVal.AddRange(subjs.OrderBy(subj => FuzzySearch.LevenshteinDistance(input, subj.Name)).Take(10).Select(subj => new InterestKeywordDto { Id = subj.Id, Slug = subj.Slug, Name = subj.Name, OriginalInput = input }));
			}

			return retVal;
		}

		public IList<string> GetSlugAndParentSlug(IList<string> slugs)
		{
			var interestandParents = _interestRepository.GetSlugAndParentSlug(slugs).ToList();
			
			interestandParents.AddRange(slugs);

			return interestandParents.Distinct().ToList();
		}

		public IList<RankedInterestDto> GetMostPopularInterests()
		{
			return _userInterestRepository.GetMostPopularInterests();
		}

		public Interest Create(string name, string relatedTo)
		{
			Interest interest = _interestRepository.GetByName(name);

			if (interest == null)
			{
                Interest relatedInterest = string.IsNullOrWhiteSpace(relatedTo) ? null : _interestRepository.GetByName(relatedTo);

				//Since the interest does not exist create it.
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
				interest = new Interest(formattedName);

                interest.ParentInterest = relatedInterest;

				_interestRepository.SaveOrUpdate(interest);
			}
			return interest;
		}

		public Interest Get(int id)
		{
			return _interestRepository.Get(id);
		}


        public IList<Interest> GetMainCategoryInterests()
        {
            return _interestRepository.GetMainCategoryInterests().ToList();
        }
		#endregion
	}
}