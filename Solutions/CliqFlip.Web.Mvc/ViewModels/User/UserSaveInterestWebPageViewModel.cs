﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Domain.Dtos;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserSaveInterestWebPageViewModel
	{
		public int UserInterestId { get; set; }

		[Required]
		public string LinkUrl { get; set; }
	}
}