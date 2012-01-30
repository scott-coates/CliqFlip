using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Search;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.TaskImpl
{
	public class InterestTasks : IInterestTasks
	{
		private readonly ILinqRepository<Interest> _interestRepository;
		//userInterestRepo is aggregate root - http://stackoverflow.com/a/5806356/173957
		private readonly ILinqRepository<UserInterest> _userInterestRepository;

		public InterestTasks(ILinqRepository<Interest> interestRepository, ILinqRepository<UserInterest> userInterestRepository)
		{
			_interestRepository = interestRepository;
			_userInterestRepository = userInterestRepository;
		}

		#region IInterestTasks Members

		public IList<InterestKeywordDto> GetMatchingKeywords(string input)
		{
			var retVal = new List<InterestKeywordDto>();

			var adHoc = new AdHoc<Interest>(s => s.Name.Contains(input) || input.Contains(s.Name));

			IList<Interest> subjs = _interestRepository.FindAll(adHoc).ToList();

			if (subjs.Any())
			{
				retVal.AddRange(subjs.OrderBy(subj => FuzzySearch.LevenshteinDistance(input, subj.Name)).Take(10).Select(subj => new InterestKeywordDto { Id = subj.Id, Slug = subj.Slug, Name = subj.Name, OriginalInput = input }));
			}

			return retVal;
		}

		public IList<string> GetSlugAndParentSlug(IList<string> slugs)
		{
			var interestsAndParentQuery = new AdHoc<Interest>(x => slugs.Contains(x.Slug) && x.ParentInterest != null);
			List<string> interestandParents = _interestRepository.FindAll(interestsAndParentQuery).Select(x => x.ParentInterest.Slug).ToList();
			interestandParents.AddRange(slugs);
			return interestandParents.Distinct().ToList();
		}

		IList<RankedInterestDto> IInterestTasks.GetMostPopularInterests()
		{
			var popularInterests = _userInterestRepository
				.FindAll().ToList()
				.GroupBy(x => x.Interest)
				.Select(x => new { x.Key, Count = x.Count() })
				.OrderByDescending(x => x.Count)
				.Take(10).ToList();

			return popularInterests.Select(x => new RankedInterestDto(x.Key.Id, x.Key.Name, x.Key.Slug, x.Count)).ToList();

		}


		public UserInterestDto GetOrCreate(string name)
		{
			var withMatchingName = new AdHoc<Interest>(x => x.Name == name);
			Interest interest = _interestRepository.FindOne(withMatchingName);

			if (interest == null)
			{
				//Since the interest does not exist create it.
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
				interest = new Interest(formattedName);

				//TODO: Turn the formatted name into the Slug format
				//TODO: relate the new Interest to the know Interest

				_interestRepository.Save(interest);
			}
			return new UserInterestDto(interest.Id, interest.Name, interest.Slug);
		}

		#endregion

		public void DoSomethingWithInterest()
		{
			throw new NotImplementedException();
		}
	}
}