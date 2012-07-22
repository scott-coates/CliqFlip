using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SharpArch.Web.Mvc.JsonNet;

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

        // GET /api/feed
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

        //// GET /api/feed/5
        //public string Get(int page)
        //{
        //    return "value";
        //}

        // POST /api/feed
        public void Post(string value)
        {
        }

        // PUT /api/feed/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/feed/5
        public void Delete(int id)
        {
        }
    }
}