using System.Linq;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;

namespace CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces
{
	public interface IPostCollectionQuery
	{
        IQueryable<UserPostApiModel> GetPostCollection(string userName, int? page);
	}
}