using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class InterestController : ApiController
    {
        private readonly IInterestTasks _interestTasks;

        public InterestController()
            : this(ServiceLocator.Current.GetInstance<IInterestTasks>())
        {
        }

        public InterestController(IInterestTasks interestTasks)
        {
            _interestTasks = interestTasks;
        }

        [HttpGet]
        [Transaction]
        public IList<InterestKeywordDto> Get(string interestName)
        {
            //TODO: put this in a view model query
            IList<InterestKeywordDto> matchingKeywords = _interestTasks.GetMatchingKeywords(interestName);

            if (!matchingKeywords.Any(x => x.Name.ToLower() == interestName.ToLower()))
            {
                string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(interestName.ToLower());
                matchingKeywords.Insert(0, new InterestKeywordDto { Name = formattedName, Slug = "-1" + interestName.ToLower() });
            }

            return matchingKeywords;
        }
    }
}