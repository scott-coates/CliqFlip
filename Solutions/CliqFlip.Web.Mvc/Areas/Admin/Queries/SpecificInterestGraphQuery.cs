using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;

namespace CliqFlip.Web.Mvc.Areas.Admin.Queries
{
	public class SpecificInterestGraphQuery : ISpecificInterestGraphQuery
	{
		private readonly IInterestTasks _interestTasks;

		public SpecificInterestGraphQuery(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		public SpecificInterestGraphViewModel GetInterestList(string interest)
		{
			_interestTasks.GetRelatedInterests(interest);
			return null;
		}
	}
}