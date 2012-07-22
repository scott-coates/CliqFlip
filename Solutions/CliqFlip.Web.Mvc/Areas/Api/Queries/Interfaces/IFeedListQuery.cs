using System.Linq;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces
{
	public interface IFeedListQuery
	{
        FeedListApiModel GetFeedList(string userName, int? page, string search);
	}
}