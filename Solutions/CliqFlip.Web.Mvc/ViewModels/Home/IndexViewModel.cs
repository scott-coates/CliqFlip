using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Web.Mvc.ViewModels.Home
{
	public class IndexViewModel
	{
		public string SubjectsJson { get; set; }

		public IndexViewModel()
		{
		}
	}
}