using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.User
{
    public class UserSearchPipelineResult
    {
        public IEnumerable<UserSearchResultDto> Users { get; set; }
        public IEnumerable<ScoredRelatedInterestDto> Interests { get; set; }

        public class UserSearchResultDto
        {
            public float MatchCount { get; set; }
            public IList<UserInterestDto> InterestDtos { get; set; }
            public string Username { get; set; }
            public string Bio { get; set; }
            public string Headline { get; set; }
            public string ImageUrl { get; set; }

            public UserSearchResultDto(Entities.User user)
            {
                Username = user.Username;
                InterestDtos = user.Interests.Select(x => new UserInterestDto(x)).ToList();
                Bio = user.Bio;
                Headline = user.Headline;
                ImageUrl = user.ProfileImage != null ? user.ProfileImage.ImageData.MediumFileName : null;
            }

            public class UserInterestDto
            {
                public string Name { get; set; }
                public string Slug { get; set; }
                public float? Passion { get; set; }
                public string DefaultImageUrl { get; set; }

                public UserInterestDto(UserInterest interests)
                {
                    Name = interests.Interest.Name;
                    Slug = interests.Interest.Slug;
                    Passion = interests.Options.Passion;
                    var defaultImage = interests.Media.FirstOrDefault() as Image;
                    DefaultImageUrl = defaultImage != null ? defaultImage.ImageData.MediumFileName : null;
                }
            }
        }
    }
}