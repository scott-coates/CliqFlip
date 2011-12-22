using System.Collections.Generic;
using CliqFlip.Domain.Dtos;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class UsersByInterestsSearchResultsViewModel
	{
		public IList<UserDto> UserDtos { get; set; }
	}
}