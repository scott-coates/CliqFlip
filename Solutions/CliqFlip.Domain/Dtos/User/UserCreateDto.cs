using System.Collections.Generic;

namespace CliqFlip.Domain.Dtos.User
{
	public class UserCreateDto
	{
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<UserAddInterestDto> InterestDtos { get; set; }
		public string Username { get; set; }

		public UserCreateDto()
		{
            InterestDtos = new List<UserAddInterestDto>();
		}
    }
}