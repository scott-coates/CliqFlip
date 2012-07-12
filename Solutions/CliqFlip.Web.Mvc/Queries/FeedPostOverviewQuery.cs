using System.Web.Mvc;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
    public class FeedPostOverviewQuery : IFeedPostOverviewQuery
    {
        private readonly IPostRepository _postRepository;

        public FeedPostOverviewQuery(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public FeedPostOverviewViewModel GetFeedPostOverview(int postId, UrlHelper url)
        {
            var retVal = new FeedPostOverviewViewModel();
            var post = _postRepository.Get(postId);
            retVal.Username = post.UserInterest.User.Username;
            retVal.Headline = post.UserInterest.User.Headline;
            retVal.ImageDescription = post.Description;
            return retVal;
        }
    }
}