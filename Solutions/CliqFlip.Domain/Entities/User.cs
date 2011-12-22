using System.Collections.Generic;
using CliqFlip.Domain.Dtos;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class User : Entity
    {
		public virtual ISet<Interest> Interests { get; set; }
		public virtual string Username { get; set; }
		
		public User()
		{
			 Interests = new HashSet<Interest>();
		}
    }
}