using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CliqFlip.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.Queries
{
	public class InterestFeedQuery : IInterestFeedQuery
	{
		private readonly IUserTasks _userTasks;
	    private readonly IPostTasks _postTasks;

		public InterestFeedQuery( IUserTasks userTasks, IPostTasks postTasks)
		{
		    _userTasks = userTasks;
		    _postTasks = postTasks;
		}

	    #region IInterestFeedQuery Members

		public InterestsFeedViewModel GetUsersByInterests(string userName, int? page, UrlHelper url)
		{
			var retVal = new InterestsFeedViewModel();

			User user = _userTasks.GetUser(userName);

            IList<UserPostDto> postDtos = _postTasks.GetPostsByInterests(user.Interests.Select(x => x.Interest).ToList());
			retVal.Total = postDtos.Count;
			retVal.Posts = postDtos
				.AsPagination( page ?? 1, Constants.FEED_LIMIT)
				.Select(x => new InterestsFeedViewModel.FeedPostViewModel(x)
				{
				    UserPageUrl = url.Action("Index", "User", new { username = x.Username }),
                    PostUrl = url.Action("Index","Post",new {post=x.Post.PostId}),
                    IsLikedByUser = x.Post.Likes.Any(y=>y.UserId == user.Id)
				})
				.ToList();

			//rank them in order then grab that page
			return retVal;
		}

		#endregion
	}
}