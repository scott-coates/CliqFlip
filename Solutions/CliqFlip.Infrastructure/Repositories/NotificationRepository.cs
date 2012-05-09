using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class NotificationRepository : LinqRepository<Notification>, INotificationRepository
	{
		public Notification GetMostRecentNotification()
		{
			return FindAll().OrderByDescending(x => x.CreateDate).FirstOrDefault();
		}
	}
}