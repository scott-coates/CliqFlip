using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Notification;
using NHibernate.Hql.Ast.ANTLR;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class NotificationController : Controller
	{
		private readonly ILatestNotificationQuery _latestNotificationQuery;
		private readonly IHttpContextProvider _httpContextProvider;

		public NotificationController(ILatestNotificationQuery latestNotificationQuery, IHttpContextProvider httpContextProvider)
		{
			_latestNotificationQuery = latestNotificationQuery;
			_httpContextProvider = httpContextProvider;
		}

		public ActionResult Latest()
		{
			var vm = _latestNotificationQuery.GetLatestNotification();

			if (vm != null)
			{
				var cookie = _httpContextProvider.Request.Cookies[Constants.NOTIFICATION_COOKIE];

				if (cookie != null && cookie.Value == vm.Id.ToString(CultureInfo.InvariantCulture))
				{
					vm = null;//Don't show this notification - they've seen it.
				}
			}

			return PartialView(vm);
		}
	}
}
