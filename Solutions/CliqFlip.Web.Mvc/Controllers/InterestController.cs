﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using Newtonsoft.Json;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class InterestController : Controller
	{
		private readonly ISubjectTasks _subjectTasks;

		public InterestController(ISubjectTasks subjectTasks)
		{
			_subjectTasks = subjectTasks;
		}

		[Transaction]
		public ActionResult KeywordSearch(string input)
		{
			IList<InterestKeywordDto> matchingKeywords = _subjectTasks.GetMatchingKeywords(input);

			var retVal = new JsonNetResult(matchingKeywords)
			             	{
			             		SerializerSettings =
			             			{
			             				NullValueHandling = NullValueHandling.Include
			             			}
			             	};

			return retVal;
		}
	}
}