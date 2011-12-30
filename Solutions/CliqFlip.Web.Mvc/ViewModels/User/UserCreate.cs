using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserCreate
    {
        public UserCreate()
        {
            Interests = new List<InterestCreate>();
        }

        [Required]
        public String Email { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }

        public List<InterestCreate> Interests { get; set; }
    }

    public class InterestCreate
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Category { get; set; }
        public int Sociality { get; set; }
    }
}