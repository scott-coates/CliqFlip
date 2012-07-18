using System.Linq;
using CliqFlip.Domain.Common;
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
        private readonly IUserInterestTasks _userInterestTasks;

        public FeedPostOverviewQuery(IPostTasks postTasks, IUserInterestTasks userInterestTasks)
        {
            _postTasks = postTasks;
            _userInterestTasks = userInterestTasks;
        }

        public FeedPostOverviewViewModel GetFeedPostOverview(int postId, User viewingUser)
        {
            var retVal = new FeedPostOverviewViewModel();
            var post = _postTasks.Get(postId);
            retVal.PostId = post.Id;
            retVal.Username = post.User.Username;
            retVal.Headline = post.User.Headline;
            retVal.AuthorImageUrl = post.User.ProfileImage != null ? post.User.ProfileImage.ImageData.MediumFileName : Constants.DEFAULT_PROFILE_IMAGE;
            retVal.ImageDescription = post.Description;
            retVal.IsLikedByUser = post.Likes.Any(x => x.User == viewingUser);

            if (post.Medium is Video)
	        {
                retVal.VideoUrl = (post.Medium as Video).VideoUrl;
	        }
            else if(post.Medium is WebPage){
                retVal.WebPageUrl = (post.Medium as WebPage).LinkUrl;
            }
            else{
                //this is an image
                retVal.ImageUrl = (post.Medium as Image).ImageData.FullFileName;
            }

            retVal.Activity = post.Comments.Select(
                x => new FeedPostOverviewViewModel.ActivityViewModel
                {
                    CommentText = x.CommentText,
                    Username = x.User.Username,
                    ProfileImageUrl = x.User.ProfileImage.ImageData.MediumFileName
                }).ToList();


            var commonInterests = _userInterestTasks.GetInterestsInCommon(viewingUser, post.User);
            retVal.CommonInterests = commonInterests.Select(x => new FeedPostOverviewViewModel.CommonInterestViewModel { Name = x.Name }).ToList();
            retVal.HasCommonIntersts = commonInterests.Any();
            return retVal;
        }
    }
}