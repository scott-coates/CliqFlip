using CliqFlip.Web.Mvc.ViewModels.Notification;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface ILatestNotificationQuery
	{
		LatestNotificationViewModel GetLatestNotification();
	}
}