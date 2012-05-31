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
        Interest Create(string name, int? relatedTo);
        Interest Get(int id);
        IList<Interest> GetMainCategoryInterests();
		IList<Interest> GetAll();
	}
}
