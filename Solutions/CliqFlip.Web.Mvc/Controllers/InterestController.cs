using System.Collections.Generic;
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
		private readonly IInterestTasks _interestTasks;

		public InterestController(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		[Transaction]
		public ActionResult KeywordSearch(string input)
		{
			IList<InterestKeywordDto> matchingKeywords = _interestTasks.GetMatchingKeywords(input);

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