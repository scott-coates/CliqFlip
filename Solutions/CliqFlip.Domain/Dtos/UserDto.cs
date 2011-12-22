using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos
{
	public class UserDto
	{
		public virtual IList<InterestDto> InterestDtos { get; set; }
		public virtual string Username { get; set; }

		public UserDto()
		{
			InterestDtos = new List<InterestDto>();
		}
	}
}