using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class UserDto
	{
		public IList<InterestDto> InterestDtos { get; set; }
		public string Username { get; set; }

		public UserDto()
		{
			InterestDtos = new List<InterestDto>();
		}
	}
}