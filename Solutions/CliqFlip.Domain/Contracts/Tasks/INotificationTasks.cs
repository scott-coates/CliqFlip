using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Tasks
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
