using System.Collections.Generic;
using CliqFlip.Domain.ReadModels;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface  INotificationTasks
	{
		Notification GetMostRecentNotification();
		IList<Notification> GetAll();
		Notification Get(int id);
		void Save(Notification notification);
		void Delete(Notification notification);
	}
}
