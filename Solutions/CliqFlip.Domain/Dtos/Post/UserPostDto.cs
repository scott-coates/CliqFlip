namespace CliqFlip.Domain.Dtos.Post
{
	public class UserPostDto
	{
		public string Username { get; set; }
		public string ProfileImageUrl { get; set; }
		public string Interest { get; set; }
        public PostDtoWithActivity Post { get; set; }

		public UserPostDto(Entities.Post post)
		{
            Post = new PostDtoWithActivity(post);
			Username = post.User.Username;
			ProfileImageUrl = post.User.ProfileImage != null ? post.User.ProfileImage.ImageData.ThumbFileName : null;
			Interest = post.Interest.Name;
		}
	}
}