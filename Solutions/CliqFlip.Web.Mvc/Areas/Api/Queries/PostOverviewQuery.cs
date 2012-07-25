using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries
{
    public class PostOverviewQuery : IPostOverviewQuery
    {
        private readonly IPostTasks _postTasks;
        private readonly IUserInterestTasks _userInterestTasks;

        public PostOverviewQuery(IPostTasks postTasks, IUserInterestTasks userInterestTasks)
        {
            _postTasks = postTasks;
            _userInterestTasks = userInterestTasks;
        }

        public PostOverviewApiModel GetPostOverview(int postId, User viewingUser)
        {
            var retVal = new PostOverviewApiModel();
            var post = _postTasks.Get(postId);
            retVal.PostId = post.Id;
            retVal.Username = post.User.Username;
            retVal.Headline = post.User.Headline;
            retVal.AuthorImageUrl = post.User.ProfileImage != null ? post.User.ProfileImage.ImageData.MediumFileName : Constants.DEFAULT_PROFILE_IMAGE;
            retVal.ImageDescription = post.Description;
            retVal.IsLikedByUser = post.Likes.Any(x => x.User == viewingUser);

            if (post.Medium is Video)
	        {
                retVal.VideoUrl = ((Video)post.Medium).VideoUrl;
	        }
            else if(post.Medium is WebPage){
                retVal.WebPageUrl = ((WebPage)post.Medium).LinkUrl;
            }
            else{
                //this is an image
                retVal.ImageUrl = ((Image)post.Medium).ImageData.FullFileName;
            }

            retVal.Activity = post.Comments.Select(
                x => new PostOverviewApiModel.ActivityViewModel
                {
                    CommentText = x.CommentText,
                    Username = x.User.Username,
                    ProfileImageUrl = x.User.ProfileImage.ImageData.MediumFileName
                }).ToList();


            var commonInterests = _userInterestTasks.GetInterestsInCommon(viewingUser, post.User);
            retVal.CommonInterests = commonInterests.Select(x => new PostOverviewApiModel.CommonInterestViewModel { Name = x.Name }).ToList();
            retVal.HasCommonIntersts = commonInterests.Any();
            return retVal;
        }
    }
}