using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Domain.Dtos;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserProfileViewModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Bio { get; set; }
		public string Headline { get; set; }
		public string InterestsJson { get; set; }
		public string SaveHeadlineUrl { get; set; }
		public string SaveBioUrl { get; set; }
		public string SaveMindMapUrl { get; set; }
		public bool CanEdit { get; set; }
	}
}