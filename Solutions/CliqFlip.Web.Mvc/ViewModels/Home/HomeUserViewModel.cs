using System;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.ViewModels.Home
{
    public class HomeUserViewModel
    {
        public string Username { get; set; }
        public int Posts { get; set; }
        public int Interests { get; set; }
        public int Friends { get; set; }
    }
}
