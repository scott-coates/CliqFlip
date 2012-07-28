using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Notification;

namespace CliqFlip.Web.Mvc.Queries
{
	public class LatestNotificationQuery : ILatestNotificationQuery
	{
		private readonly INotificationTasks _notificationTasks;

		public LatestNotificationQuery(INotificationTasks notificationTasks)
		{
			_notificationTasks = notificationTasks;
		}

		#region ILatestNotificationQuery Members

		public LatestNotificationViewModel GetLatestNotification()
		{
			Notification notification = _notificationTasks.GetMostRecentNotification();
			LatestNotificationViewModel vm = null;
			if (notification != null)
			{
				vm = new LatestNotificationViewModel {Id = notification.Id, Message = notification.Message,NotificationCookie = Constants.NOTIFICATION_COOKIE};
			}
			return vm;
		}

		#endregion
	}
}