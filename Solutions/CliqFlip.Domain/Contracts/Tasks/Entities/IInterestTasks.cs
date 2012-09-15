using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.ReadModels;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface IInterestTasks
	{
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
		Interest Create(string name, int? relatedTo);
		Interest Get(int id);
		IList<Interest> GetMainCategoryInterests();
		IList<Interest> GetAll();
        Interest GetByName(string name);
		RelatedInterestListDto GetRelatedInterests(string interestSlug);
		void CreateRelationships(RelatedInterestListDto relatedInterestListDto);
	    int UploadInterests(FileStreamDto fileStream);
	}
}