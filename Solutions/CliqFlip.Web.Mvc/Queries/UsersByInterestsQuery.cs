using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UsersByInterestsQuery : IUsersByInterestsQuery
	{
		private readonly IUserTasks _userTasks;
        private readonly IHttpContextProvider _httpProvider;
		public UsersByInterestsQuery(IUserTasks userTasks, IHttpContextProvider httpProvider)
		{
			_userTasks = userTasks;
            _httpProvider = httpProvider;
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
                var indvResultViewModel = new UsersByInterestViewModel.IndividualResultViewModel(user, aliasCollection);
                if (indvResultViewModel.ImageUrl == null)
	            {
                    indvResultViewModel.ImageUrl = UrlHelper.GenerateContentUrl(Constants.DEFAULT_PROFILE_IMAGE, _httpProvider.Context);
	            }
				retVal.Results.Add(indvResultViewModel);
			}

			retVal.PagedResults = retVal.Results.AsPagination(page ?? 1, 5);
			return retVal;
		}

		#endregion
	}
}