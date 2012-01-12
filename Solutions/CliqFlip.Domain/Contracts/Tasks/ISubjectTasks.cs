using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface ISubjectTasks
	{
		IList<InterestKeywordDto> GetMatchingKeywords(string input);
		IList<string> GetSystemAliasAndParentAlias(IList<string> systemAliases);
        InterestDto SaveOrUpdate(string name);
    }
}
