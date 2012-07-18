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
        public PostDtoWithActivity Post { get; set; }

		public InterestFeedItemDto(Entities.Post post)
		{
            Post = new PostDtoWithActivity(post);
			Username = post.User.Username;
			ProfileImageUrl = post.User.ProfileImage != null ? post.User.ProfileImage.ImageData.ThumbFileName : null;
			Interest = post.Interest.Name;
		}
	}
}