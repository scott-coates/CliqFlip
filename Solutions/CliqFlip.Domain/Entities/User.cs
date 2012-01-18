using System.Collections.Generic;
using CliqFlip.Domain.Dtos;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class User : Entity
    {
        public virtual IList<UserInterest> Interests { get; set; }
		public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
		public virtual string Bio { get; set; }
		
		public User()
		{
            Interests = new List<UserInterest>();
		}

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            Interests = new List<UserInterest>();
        }
    }
}