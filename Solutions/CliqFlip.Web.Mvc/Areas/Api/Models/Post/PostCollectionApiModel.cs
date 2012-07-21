using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Dtos.Post;
using Newtonsoft.Json;

namespace CliqFlip.Web.Mvc.Areas.Api.Models.Post
{
	public class PostCollectionApiModel
	{
		[JsonProperty("total")]
		public int Total { get; set; }

		[JsonProperty("data")]
		public IList<PostApiModel> Posts { get; set; }

        public PostCollectionApiModel()
		{
			Posts = new List<PostApiModel>();
		}

        public class PostApiModel : Post.PostApiModel  
		{
			public string Username { get; set; }
			public string UserPageUrl { get; set; }
			public string ProfileImageUrl { get; set; }
			public string Interest { get; set; }
			public string PostUrl { get; set; }
			public int CommentCount { get; set; }
            public IList<PostCommentApiModel> Comments { get; set; } 
            public bool IsLikedByUser { get; set; }

            public PostApiModel(UserPostDto userPostDto): base(userPostDto.Post)
			{
				Username = userPostDto.Username;
                ProfileImageUrl = userPostDto.ProfileImageUrl ?? Constants.DEFAULT_PROFILE_IMAGE;
				Interest = userPostDto.Interest;

			    CommentCount = userPostDto.Post.CommentCount;
                Comments = userPostDto.Post.Comments.Take(2).Select(x => new PostCommentApiModel
			    {
			        CommentText = x.CommentText, Username = x.Username
			    }).ToList();
			}
		}

		public class PostCommentApiModel
		{
            public string Username { get; set; }
            public string CommentText { get; set; }
		}
	}
}