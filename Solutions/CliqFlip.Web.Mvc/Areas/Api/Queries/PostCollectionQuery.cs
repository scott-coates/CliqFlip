using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Helpers.Url.Interfaces;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries
{
    public class PostCollectionQuery : IPostCollectionQuery
    {
        private readonly IUserTasks _userTasks;
        private readonly IPostTasks _postTasks;
        private readonly IMvcUrlHelperProvider _mvcUrlHelperProvider;

        public PostCollectionQuery(IUserTasks userTasks, IPostTasks postTasks, IMvcUrlHelperProvider mvcUrlHelperProvider)
        {
            _userTasks = userTasks;
            _postTasks = postTasks;
            _mvcUrlHelperProvider = mvcUrlHelperProvider;
        }

        public IQueryable<UserPostApiModel> GetPostCollection(string userName, int? page)
        {
            var urlHelper = _mvcUrlHelperProvider.ProvideUrlHelper();

            User user = _userTasks.GetUser(userName);

            IList<UserPostDto> postDtos = _postTasks.GetPostsByInterests(user.Interests.Select(x => x.Interest).ToList());
            IQueryable<UserPostApiModel> retVal = postDtos
                .Select(
                    x => new UserPostApiModel(x)
                    {
                        UserPageUrl = urlHelper.Action("Index", "User", new { username = x.Username }),
                        PostUrl = urlHelper.Action("Index", "Post", new { post = x.Post.PostId }),
                        IsLikedByUser = x.Post.Likes.Any(y => y.UserId == user.Id)
                    })
                .AsQueryable();

            //rank them in order then grab that page
            return retVal;
        }
    }
}