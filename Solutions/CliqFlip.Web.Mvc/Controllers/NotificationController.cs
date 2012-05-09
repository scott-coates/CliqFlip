using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Notification;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class NotificationController : Controller
    {
    	private readonly ILatestNotificationQuery _latestNotificationQuery;

    	public NotificationController(ILatestNotificationQuery latestNotificationQuery)
    	{
    		_latestNotificationQuery = latestNotificationQuery;
    	}

    	public ActionResult Latest()
    	{
			var vm = _latestNotificationQuery.GetLatestNotification();
            return PartialView(vm);
        }
    }
}
