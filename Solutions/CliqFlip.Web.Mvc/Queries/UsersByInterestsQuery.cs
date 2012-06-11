using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;

using CliqFlip.Domain.Dtos.User;
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
	    private readonly IInterestTasks _interestTasks;
        private readonly IHttpContextProvider _httpProvider;
		public UsersByInterestsQuery(IUserTasks userTasks, IHttpContextProvider httpProvider, IInterestTasks interestTasks)
		{
			_userTasks = userTasks;
            _httpProvider = httpProvider;
		    _interestTasks = interestTasks;
		}

		#region IUsersByInterestsQuery Members

		public UsersByInterestViewModel GetGetUsersByInterests(string slugs, int? page)
		{
            //NOTE: The slug string was lowered cased because if someone changed 'software' to 'Software' in the query string
            //      no matches would be found.
			List<string> aliasCollection = slugs
				.ToLower()
				.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();

            var relatedInterests = _interestTasks.GetRelatedInterests(aliasCollection);

            IList<OldUserSearchByInterestsDto> users = _userTasks.GetUsersByInterestsDtos(relatedInterests);
			var retVal = new UsersByInterestViewModel();
			foreach (OldUserSearchByInterestsDto user in users)
			{
                var indvResultViewModel = new UsersByInterestViewModel.IndividualResultViewModel(user, relatedInterests.Select(x=>x.Slug).ToList());
                if (indvResultViewModel.ImageUrl == null)
	            {
                    indvResultViewModel.ImageUrl = UrlHelper.GenerateContentUrl(Constants.DEFAULT_PROFILE_IMAGE, _httpProvider.Context);
	            }
				retVal.Results.Add(indvResultViewModel);
			}

			retVal.PagedResults = retVal.Results.AsPagination(page ?? 1, 8);
			return retVal;
		}

		#endregion
	}
}