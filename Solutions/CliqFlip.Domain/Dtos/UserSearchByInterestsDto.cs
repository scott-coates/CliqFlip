using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class UserSearchByInterestsDto
	{
		public UserDto UserDto { get; set; }
		public int MatchCount { get; set; }
	}
}