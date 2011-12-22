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
		public IList<InterestDto> InterestDtos { get; set; }

		public IndexViewModel()
		{
			InterestDtos = new List<InterestDto>();
		}
	}
}