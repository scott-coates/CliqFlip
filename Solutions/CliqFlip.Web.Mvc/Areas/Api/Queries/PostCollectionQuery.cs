using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries
{
    public class PostCollectionQuery : IPostCollectionQuery
    {
        private readonly IUserTasks _userTasks;
        private readonly IPostTasks _postTasks;

        public PostCollectionQuery(IUserTasks userTasks, IPostTasks postTasks)
        {
            _userTasks = userTasks;
            _postTasks = postTasks;
        }

        public PostCollectionApiModel GetPostCollection(string userName, int? page, UrlHelper url)
        {
            var retVal = new PostCollectionApiModel();

            User user = _userTasks.GetUser(userName);

            IList<UserPostDto> postDtos = _postTasks.GetPostsByInterests(user.Interests.Select(x => x.Interest).ToList());
            retVal.Total = postDtos.Count;
            retVal.Posts = postDtos
                .AsPagination(page ?? 1, Constants.FEED_LIMIT)
                .Select(
                    x => new PostCollectionApiModel.PostApiModel(x)
                    {
                        UserPageUrl = url.Link("User", new { username = x.Username }),
                        PostUrl = url.Link("Post", new { post = x.Post.PostId }),
                        IsLikedByUser = x.Post.Likes.Any(y => y.UserId == user.Id)
                    })
                .ToList();

            //rank them in order then grab that page
            return retVal;
        }
    }
}