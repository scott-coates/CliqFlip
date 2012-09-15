using System.Collections.Generic;
using System.Linq;
using CliqFlip.Common;

namespace CliqFlip.Domain.Dtos.User
{
    public class UserSearchResultDto
    {
        public float Score { get; set; }
        public IList<UserInterestDto> InterestDtos { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string ThumbImageUrl { get; set; }
        public string FullImageUrl { get; set; }
        public string MajorLocationName { get; set; }
        public IList<InterestInCommonDto> InterestsInCommon { get; set; }
        public int DirectInterestCount { get; set; }
        public int IndirectInterestCount { get; set; }
        public int CommonInterestCount { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
         
        public UserSearchResultDto(ReadModels.User user)
        {
            Username = user.Username;
            FirstName = user.FirstName;
            InterestDtos = user.Interests.Select(x => new UserInterestDto(x)).ToList();
            ThumbImageUrl = user.ProfileImage != null ? user.ProfileImage.ImageData.ThumbFileName : Constants.DEFAULT_PROFILE_IMAGE;
            FullImageUrl = user.ProfileImage != null ? user.ProfileImage.ImageData.FullFileName : Constants.DEFAULT_PROFILE_IMAGE;
            MajorLocationName = user.Location.MajorLocation.Name;
            Latitude = user.Location.Data.Latitude;
            Longitude = user.Location.Data.Longitude;
        }

        #region Nested type: InterestInCommonApiModel

        public class InterestInCommonDto
        {
            public string Name { get; set; }
            public bool IsExactMatch { get; set; }
        }

        #endregion

        #region Nested type: UserInterestDto

        public class UserInterestDto
        {
            public string Name { get; set; }
            public string Slug { get; set; }
            public int InterestId { get; set; }
 
            public UserInterestDto(ReadModels.UserInterest interest)
            {
                Name = interest.Interest.Name;
                Slug = interest.Interest.Slug;
                InterestId = interest.Interest.Id;
            }
        }

        #endregion
    }
}