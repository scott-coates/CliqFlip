using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface IInterestTasks
	{
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
		IList<string> GetSlugAndParentSlug(IList<string> slugs);
		IList<RankedInterestDto> GetMostPopularInterests();
        UserInterestDto GetOrCreate(string name);
    }
}
