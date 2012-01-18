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

namespace CliqFlip.Tasks.TaskImpl
{
	public class InterestTasks : IInterestTasks
	{
		private readonly ILinqRepository<Interest> _repository;

		public InterestTasks(ILinqRepository<Interest> repository)
		{
			_repository = repository;
		}

		#region IInterestTasks Members

		public IList<InterestKeywordDto> GetMatchingKeywords(string input)
		{
			var retVal = new List<InterestKeywordDto>();

			var adHoc = new AdHoc<Interest>(s => s.Name.Contains(input) || input.Contains(s.Name));

			IList<Interest> subjs = _repository.FindAll(adHoc).ToList();

			if (subjs.Any())
			{
				retVal.AddRange(subjs.OrderBy(subj => FuzzySearch.LevenshteinDistance(input, subj.Name)).Take(10).Select(subj => new InterestKeywordDto { Id = subj.Id, SystemAlias = subj.SystemAlias, Name = subj.Name, OriginalInput = input }));
			}

			return retVal;
		}

		public IList<string> GetSystemAliasAndParentAlias(IList<string> systemAliases)
		{
			var interestsAndParentQuery = new AdHoc<Interest>(x => systemAliases.Contains(x.SystemAlias) && x.ParentInterest != null);
			List<string> interestandParents = _repository.FindAll(interestsAndParentQuery).Select(x => x.ParentInterest.SystemAlias).ToList();
			interestandParents.AddRange(systemAliases);
			return interestandParents.Distinct().ToList();
		}


		public InterestDto GetOrCreate(string name)
		{
			var withMatchingName = new AdHoc<Interest>(x => x.Name == name);
			Interest interest = _repository.FindOne(withMatchingName);

			if (interest == null)
			{
				//Since the interest does not exist create it.
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
				interest = new Interest(formattedName);

				//TODO: Turn the formatted name into the SystemAlias format
				//TODO: relate the new Interest to the know Interest

				_repository.Save(interest);
			}
			return new InterestDto(interest.Id, interest.Name, interest.SystemAlias);
		}

		#endregion

		public void DoSomethingWithInterest()
		{
			throw new NotImplementedException();
		}
	}
}