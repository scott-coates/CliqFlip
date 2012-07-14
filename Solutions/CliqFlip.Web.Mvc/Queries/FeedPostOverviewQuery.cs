using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
    public class FeedPostOverviewQuery : IFeedPostOverviewQuery
    {
        private readonly IPostTasks _postTasks;

        public FeedPostOverviewQuery(IPostTasks postTasks)
        {
            _postTasks = postTasks;
        }

        public FeedPostOverviewViewModel GetFeedPostOverview(int postId, UrlHelper url)
        {
            var retVal = new FeedPostOverviewViewModel();
            var post = _postTasks.Get(postId);
            retVal.Username = post.UserInterest.User.Username;
            retVal.Headline = post.UserInterest.User.Headline;
            retVal.AuthorImageUrl = post.UserInterest.User.ProfileImage.ImageData.FullFileName;
            retVal.ImageDescription = post.Description;
            var image = post.Medium as Image;
            if(image != null)
            {
                retVal.ImageUrl = image.ImageData.FullFileName;
            }

            retVal.Activity = post.Comments.Select(
                x => new FeedPostOverviewViewModel.ActivityViewModel
                {
                    CommentText = x.CommentText,
                    Username = x.User.Username,
                    ProfileImageUrl = x.User.ProfileImage.ImageData.MediumFileName
                }).ToList();

            return retVal;
        }
    }
}