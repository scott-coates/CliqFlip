using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Helpers.Url.Interfaces;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries
{
    public class FeedListQuery : IFeedListQuery
    {
        private readonly IUserTasks _userTasks;
        private readonly IPostTasks _postTasks;
        private readonly IMvcUrlHelperProvider _mvcUrlHelperProvider;
        private readonly IUserSearchPipeline _userSearchPipeline;

        public FeedListQuery(IUserTasks userTasks, IPostTasks postTasks, IMvcUrlHelperProvider mvcUrlHelperProvider, IUserSearchPipeline userSearchPipeline)
        {
            _userTasks = userTasks;
            _postTasks = postTasks;
            _mvcUrlHelperProvider = mvcUrlHelperProvider;
            _userSearchPipeline = userSearchPipeline;
        }

        public FeedListApiModel GetFeedList(string userName, int? page, string search)
        {
            var retVal = new FeedListApiModel();
            User user = _userTasks.GetUser(userName);
            var urlHelper = _mvcUrlHelperProvider.ProvideUrlHelper();
            if (!string.IsNullOrWhiteSpace(search))
            {
                //NOTE: The slug string was lowered cased because if someone changed 'software' to 'Software' in the query string
                //      no matches would be found.
                List<string> aliasCollection = search
                    .ToLower()
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                var request = new UserSearchPipelineRequest
                {
                    User = user,
                    InterestSearch = aliasCollection,
                    LocationData = user.Location.Data
                };
                var result = _userSearchPipeline.Execute(request);
                retVal.Total = result.Users.Count;
                retVal.FeedItems.AddRange(
                    result.Users.Select(
                        x => new UserFeedItemApiModel
                        {
                            ProfileImageUrl = x.ImageUrl,
                            Username = x.Username,
                            UserPageUrl = urlHelper.Action("Index", "User", new { username = x.Username })
                        }).Skip(((page ?? 1) - 1) * Constants.FEED_LIMIT).Take(Constants.FEED_LIMIT));

                retVal.TotalReturned = retVal.FeedItems.Count;
            }
            else
            {
                IList<UserPostDto> postDtos = _postTasks.GetPostsByInterests(user.Interests.Select(x => x.Interest).ToList());
                var dtos = postDtos
                    .Select(
                        x => new PostFeedItemApiModel(x)
                        {
                            UserPageUrl = urlHelper.Action("Index", "User", new { username = x.Username }),
                            PostUrl = urlHelper.Action("Index", "Post", new { post = x.Post.PostId }),
                            IsLikedByUser = x.Post.Likes.Any(y => y.UserId == user.Id)
                        }).Skip(((page ?? 1) - 1) * Constants.FEED_LIMIT).Take(Constants.FEED_LIMIT)
                    .ToList();

                retVal.FeedItems.AddRange(dtos);
                retVal.Total = postDtos.Count;
                retVal.TotalReturned = dtos.Count;
            }
            return retVal;
        }
    }
}