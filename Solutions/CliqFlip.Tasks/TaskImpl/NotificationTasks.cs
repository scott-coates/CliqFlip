using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
	public class NotificationTasks : INotificationTasks
	{
		private readonly INotificationRepository _notificationRepository;

		public NotificationTasks(INotificationRepository notificationRepository)
		{
			_notificationRepository = notificationRepository;
		}

		public Notification GetMostRecentNotification()
		{
			throw new System.NotImplementedException();
		}

		public IList<Notification> GetAll()
		{
			return _notificationRepository.GetAll();
		}

		public Notification Get(int id)
		{
			return _notificationRepository.Get(id);
		}

		public void Save(Notification notification)
		{
			_notificationRepository.SaveOrUpdate(notification);
		}

		public void Delete(Notification notification)
		{
			_notificationRepository.Delete(notification);
		}
	}
}