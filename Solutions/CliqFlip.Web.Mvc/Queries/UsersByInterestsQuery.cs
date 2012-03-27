using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UsersByInterestsQuery : IUsersByInterestsQuery
	{
		private readonly IUserTasks _userTasks;

		public UsersByInterestsQuery(IUserTasks userTasks)
		{
			_userTasks = userTasks;
		}

		#region IUsersByInterestsQuery Members

		public UsersByInterestViewModel GetGetUsersByInterests(string slugs, int? page)
		{
            //NOTE: The slug string was lowered cased because if someone changed 'software' to 'Software' in the query string
            //      no matches would be found.
			List<string> aliasCollection = slugs.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Where(x => !x.StartsWith("-1")).ToList();

			IList<UserSearchByInterestsDto> users = _userTasks.GetUsersByInterestsDtos(aliasCollection);
			var retVal = new UsersByInterestViewModel();
			foreach (UserSearchByInterestsDto user in users)
			{
				retVal.Results.Add(new UsersByInterestViewModel.IndividualResultViewModel
									{
                                        Headline = user.UserDto.Headline,
										Name = user.UserDto.Username,
										Bio = user.UserDto.Bio,
										ResultInterestViewModels = user.UserDto.InterestDtos
											.Select(x => new UsersByInterestViewModel.IndividualResultInterestViewModel
															{
																InterestName = x.Name,
																IsMatch = aliasCollection.Contains(x.Slug.ToLower()),
                                                                Passion = x.Passion
															}).OrderByDescending(x => x.IsMatch).ThenByDescending(x => x.Passion).Take(5).ToList()
									});
			}

			retVal.PagedResults = retVal.Results.AsPagination(page ?? 1, 5);
			return retVal;
		}

		#endregion
	}
}