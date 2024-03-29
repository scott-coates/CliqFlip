﻿using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface IInterestTasks
	{
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
		Interest Create(string name, int? relatedTo);
		Interest Get(int id);
		IList<Interest> GetMainCategoryInterests();
		IList<Interest> GetAll();
		RelatedInterestListDto GetRelatedInterests(string interestSlug);
		void CreateRelationships(RelatedInterestListDto relatedInterestListDto);
	    int UploadInterests(FileStreamDto fileStream);
	}
}