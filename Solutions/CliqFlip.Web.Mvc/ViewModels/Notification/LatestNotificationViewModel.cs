using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CliqFlip.Web.Mvc.ViewModels.Notification
{
	public class LatestNotificationViewModel
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public string NotificationCookie { get; set; }
	}
}