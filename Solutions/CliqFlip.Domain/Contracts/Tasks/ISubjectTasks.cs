using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface ISubjectTasks
	{
		string GetSubjectJson();
		IList<InterestDto> GetSubjectDtos();
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
        InterestDto SaveOrUpdate(string name);
    }
}
