﻿using System.Collections.Generic;
using System.Linq;

using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface INotificationRepository : IRepository<Notification>
	{
		Notification GetMostRecentNotification();
	}
}