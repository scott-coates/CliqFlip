using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class InterestFeedItemDto
	{
		public string Username { get; set; }
		public string ImageUrl { get; set; }
		public MediumDto Medium { get; set; }

		public InterestFeedItemDto(Medium medium)
		{
			Medium = new MediumDto(medium);
			Username = medium.UserInterest.User.Username;
			ImageUrl = medium.UserInterest.User.ProfileImage != null ? medium.UserInterest.User.ProfileImage.ImageData.MediumFileName : null;
		}
	}
}