using System.Collections.Generic;
using CliqFlip.Domain.Dtos;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class UsersByInterestsSearchResultsViewModel
	{
		public IList<UserSearchByInterestsDto> UserDtos { get; set; }
	}
}