using System.Web.Http.Routing;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces
{
	public interface IPostCollectionQuery
	{
        PostCollectionApiModel GetPostCollection(string userName, int? page, UrlHelper url);
	}
}