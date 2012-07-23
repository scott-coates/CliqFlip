using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Dtos.Post;

namespace CliqFlip.Web.Mvc.Areas.Api.Models.Feed
{
    public class PostFeedItemApiModel
    {
        public string FeedItemType //TODO:look into static property - json.net isn't serializing for some reason
        {
            get { return "Post"; }
        }

        public string Username { get; set; }
        public string UserPageUrl { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Interest { get; set; }
        public string PostUrl { get; set; }
        public int CommentCount { get; set; }
        public IList<PostCommentApiModel> Comments { get; set; }
        public bool IsLikedByUser { get; set; }
        public int PostId { get; set; }
        public string MediumType { get; set; }
        public string Description { get; set; }
        public string ThumbImage { get; set; }
        public string MediumImage { get; set; }
        public string FullImage { get; set; }
        public string VideoUrl { get; set; }
        public string WebSiteUrl { get; set; }
        public string Title { get; set; }

        public PostFeedItemApiModel(UserPostDto userPostDto)
        {
            PostId = userPostDto.Post.PostId;
            MediumType = userPostDto.Post.MediumType;
            Description = userPostDto.Post.Description;
            ThumbImage = userPostDto.Post.ThumbImage;
            MediumImage = userPostDto.Post.MediumImage;
            FullImage = userPostDto.Post.FullImage;
            VideoUrl = userPostDto.Post.VideoUrl;
            WebSiteUrl = userPostDto.Post.WebSiteUrl;
            Title = userPostDto.Post.Title;
            Username = userPostDto.Username;
            ProfileImageUrl = userPostDto.ProfileImageUrl ?? Constants.DEFAULT_PROFILE_IMAGE;
            Interest = userPostDto.Interest;

            CommentCount = userPostDto.Post.CommentCount;
            Comments = userPostDto.Post.Comments.Take(2).Select(
                x => new PostCommentApiModel
                {
                    CommentText = x.CommentText,
                    Username = x.Username
                }).ToList();
        }

        public class PostCommentApiModel
        {
            public string Username { get; set; }
            public string CommentText { get; set; }
        }
    }
}