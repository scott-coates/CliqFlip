using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.Interest
{
	public class InterestFeedItemDto
	{
		public string Username { get; set; }
		public string ImageUrl { get; set; }
		public string Interest { get; set; }
		public PostDto Post { get; set; }

		public InterestFeedItemDto(Post post)
		{
			Post = new PostDto(post);
			Username = post.UserInterest.User.Username;
			ImageUrl = post.UserInterest.User.ProfileImage != null ? post.UserInterest.User.ProfileImage.ImageData.ThumbFileName : null;
			Interest = post.UserInterest.Interest.Name;
		}
	}
}