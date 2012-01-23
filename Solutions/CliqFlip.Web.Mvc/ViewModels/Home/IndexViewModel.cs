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
		public IndexViewModel()
		{
			TagCloudInterests = new List<TagCloudInterestsViewModel>();
		}

		public string KeywordSearchUrl { get; set; }
		public IList<TagCloudInterestsViewModel> TagCloudInterests { get; set; }

		public class TagCloudInterestsViewModel
		{
			public string Name { get; set; }
			public string Slug { get; set; }
			public int Weight { get; set; }
		}
	}
}