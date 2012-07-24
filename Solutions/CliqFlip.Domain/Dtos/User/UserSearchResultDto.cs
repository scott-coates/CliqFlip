using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.User
{
    public class UserSearchResultDto
    {
        public float Score { get; set; }
        public IList<UserInterestDto> InterestDtos { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string MajorLocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public UserSearchResultDto(Entities.User user)
        {
            Username = user.Username;
            InterestDtos = user.Interests.Select(x => new UserInterestDto(x)).ToList();
            Bio = user.Bio;
            Headline = user.Headline;
            ImageUrl = user.ProfileImage != null ? user.ProfileImage.ImageData.MediumFileName : null;
            Latitude = user.Location.Data.Latitude;
            Longitude = user.Location.Data.Longitude;
            MajorLocationName = user.Location.MajorLocation.Name;
        }

        public class UserInterestDto
        {
            public string Name { get; set; }
            public string Slug { get; set; }
            public float? Passion { get; set; }
            public string DefaultImageUrl { get; set; }
            public int InterestId { get; set; }

            public UserInterestDto(Entities.UserInterest interest)
            {
                Name = interest.Interest.Name;
                Slug = interest.Interest.Slug;
                Passion = interest.Options.Passion;
                Entities.Post firstPost = interest.User.Posts.FirstOrDefault(x => x.Interest.Id == interest.Interest.Id);
                if (firstPost != null)
                {
                    var defaultImage = firstPost.Medium as Image;
                    DefaultImageUrl = defaultImage != null ? defaultImage.ImageData.MediumFileName : null;
                }

                InterestId = interest.Interest.Id;
            }
        }
    }
}