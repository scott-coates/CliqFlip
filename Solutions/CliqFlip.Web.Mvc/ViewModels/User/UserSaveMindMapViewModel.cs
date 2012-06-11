﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserSaveMindMapViewModel
	{
		public int Id { get; set; }
		public float Passion { get; set; }
		public float XAxis { get; set; }
		public float YAxis { get; set; }
	}
}