using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class UserCreateDto
	{
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<UserInterestDto> InterestDtos { get; set; }
		public string Username { get; set; }

		public UserCreateDto()
		{
			InterestDtos = new List<UserInterestDto>();
		}
    }
}