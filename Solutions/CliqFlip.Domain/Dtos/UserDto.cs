using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class UserDto
	{
        public string Email { get; set; }
        public String Password { get; set; }
        public IList<UserInterestDto> InterestDtos { get; set; }
		public string Username { get; set; }
		public string Bio { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }

		public UserDto()
		{
			InterestDtos = new List<UserInterestDto>();
		}

        public UserDto(User user)
        {
            Username = user.Username;
            InterestDtos = user.Interests.Select(x => new UserInterestDto(x)).ToList();
			Bio = user.Bio;
            Headline = user.Headline;
            ImageUrl = user.ProfileImage != null ? user.ProfileImage.Data.MediumFileName : null;
        }
    }
}