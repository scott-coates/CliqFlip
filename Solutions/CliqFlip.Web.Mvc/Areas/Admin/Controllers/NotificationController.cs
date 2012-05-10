using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Security.Attributes;
using SharpArch.NHibernate.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[FormsAuthReadUserData(Order = 0)]
	[Authorize(Roles = "Administrator,Management", Order = 1)]
    public class NotificationController : Controller
    {
    	private readonly INotificationTasks _notificationTasks;

    	public NotificationController(INotificationTasks notificationTasks)
    	{
    		_notificationTasks = notificationTasks;
    	}

    	//
        // GET: /Notifications/

		[Transaction]
        public ViewResult Index()
        {
            return View(_notificationTasks.GetAll());
        }

        //
        // GET: /Notifications/Details/5
		[Transaction]
        public ViewResult Details(int id)
        {
            return View(_notificationTasks.Get(id));
        }

        //
        // GET: /Notifications/Create

		public ActionResult Create()
		{
			var notification = new Notification {CreateDate = DateTime.UtcNow};
            return View(notification);
        } 

        //
        // POST: /Notifications/Create

		[Transaction]
		[HttpPost]
		[ValidateInput(false)]
        public ActionResult Create(Notification notification)
        {
			if (ModelState.IsValid)
			{
				notification.CreateDate = DateTime.UtcNow;
				_notificationTasks.Save(notification);
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
        }
        
        //
        // GET: /Notifications/Edit/5

		[Transaction]
		public ActionResult Edit(int id)
        {
			return View(_notificationTasks.Get(id));
        }

        //
        // POST: /Notifications/Edit/5

		[Transaction]
		[HttpPost]
		[ValidateInput(false)]
        public ActionResult Edit(Notification notification)
        {
			if (ModelState.IsValid)
			{
				_notificationTasks.Save(notification);
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
        }

        //
        // GET: /Notifications/Delete/5

		[Transaction]
		public ActionResult Delete(int id)
        {
			return View(_notificationTasks.Get(id));
        }

        //
        // POST: /Notifications/Delete/5

		[Transaction]
		[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
        	_notificationTasks.Delete(_notificationTasks.Get(id));

			return RedirectToAction("Index");
        }
    }
}

