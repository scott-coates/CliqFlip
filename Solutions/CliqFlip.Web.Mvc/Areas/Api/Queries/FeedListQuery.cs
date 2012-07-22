using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
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

        public FeedListQuery(IUserTasks userTasks, IPostTasks postTasks, IMvcUrlHelperProvider mvcUrlHelperProvider)
        {
            _userTasks = userTasks;
            _postTasks = postTasks;
            _mvcUrlHelperProvider = mvcUrlHelperProvider;
        }

        public FeedListApiModel GetFeedList(string userName, int? page)
        {
            var retVal = new FeedListApiModel();
            var urlHelper = _mvcUrlHelperProvider.ProvideUrlHelper();

            User user = _userTasks.GetUser(userName);
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
            return retVal;
        }
    }
}