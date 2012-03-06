using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CliqFlip.Infrastructure.Web.Interfaces
{
	public interface IViewRenderer
	{
		string RenderView(Controller controller, string viewName, object model);
	}
}
