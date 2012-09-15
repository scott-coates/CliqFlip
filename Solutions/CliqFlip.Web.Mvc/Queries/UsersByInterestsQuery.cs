using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Common;

namespace CliqFlip.Web.Mvc.Queries
{
    public class UsersByInterestsQuery : IUsersByInterestsQuery
    {
        private readonly IUserTasks _userTasks;
        private readonly IHttpContextProvider _httpProvider;
        private readonly IUserSearchPipeline _userSearchPipeline;
        public UsersByInterestsQuery(IUserTasks userTasks, IHttpContextProvider httpProvider, IUserSearchPipeline userSearchPipeline)
        {
            _userTasks = userTasks;
            _httpProvider = httpProvider;
            _userSearchPipeline = userSearchPipeline;
        }

        #region IUsersByInterestsQuery Members

        public UsersByInterestViewModel GetGetUsersByInterests(string slugs, int? page, string username)
        {
            var retVal = new UsersByInterestViewModel();

            var user = _userTasks.GetUser(username);

            //NOTE: The slug string was lowered cased because if someone changed 'software' to 'Software' in the query string
            //      no matches would be found.
            List<string> aliasCollection = slugs
                .ToLower()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var request = new UserSearchPipelineRequest
            {
                User = user, 
                InterestSearch = aliasCollection, 
                LocationData = user.Location.Data
            };

            var userSearchPipelineResult = _userSearchPipeline.Execute(request);

            //assume interests is ordered highets to smallest
            List<string> interests = userSearchPipelineResult
                .ScoredInterests
                .Select(x => x.Slug)
                .Reverse()
                .ToList();

            foreach (var foundUser in userSearchPipelineResult.Users)
            {
                var indvResultViewModel = new UsersByInterestViewModel.IndividualResultViewModel(foundUser, interests);
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