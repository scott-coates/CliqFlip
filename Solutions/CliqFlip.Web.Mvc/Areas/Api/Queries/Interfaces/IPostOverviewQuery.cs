using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces
{
	public interface IPostOverviewQuery
	{
		PostOverviewApiModel GetPostOverview(int postId, User viewingUser);
	}
}