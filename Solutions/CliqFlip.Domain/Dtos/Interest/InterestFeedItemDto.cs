using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.Interest
{
	public class InterestFeedItemDto
	{
		public string Username { get; set; }
		public string ProfileImageUrl { get; set; }
		public string Interest { get; set; }
        public PostDtoWithComments Post { get; set; }

		public InterestFeedItemDto(Entities.Post post)
		{
            Post = new PostDtoWithComments(post);
			Username = post.UserInterest.User.Username;
			ProfileImageUrl = post.UserInterest.User.ProfileImage != null ? post.UserInterest.User.ProfileImage.ImageData.ThumbFileName : null;
			Interest = post.UserInterest.Interest.Name;
		}
	}
}