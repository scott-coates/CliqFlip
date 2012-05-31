using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;

namespace CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces
{
	public interface IInterestListQuery
	{
		InterestListViewModel GetInterestList(int? page, string orderBy);
	}
}