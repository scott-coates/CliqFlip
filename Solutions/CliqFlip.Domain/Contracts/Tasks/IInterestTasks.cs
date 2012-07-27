using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface IInterestTasks
	{
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
		IList<RankedInterestDto> GetMostPopularInterests();
		Interest Create(string name, int? relatedTo);
		Interest Get(int id);
		IList<Interest> GetMainCategoryInterests();
		IList<Interest> GetAll();
		RelatedInterestListDto GetRelatedInterests(string interestSlug);
		void CreateRelationships(RelatedInterestListDto relatedInterestListDto);
	    int UploadInterests(FileStreamDto fileStream);
        IList<ScoredRelatedInterestDto> CalculateRelatedInterestScore(IList<WeightedRelatedInterestDto> relatedInterests);
	}
}