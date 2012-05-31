using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;

namespace CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces
{
	public interface ISpecificInterestGraphQuery
	{
		SpecificInterestGraphViewModel GetInterestList(string interest);
	}
}