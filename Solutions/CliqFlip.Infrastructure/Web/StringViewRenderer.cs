using System.IO;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Web.Interfaces;

namespace CliqFlip.Infrastructure.Web
{
	public class StringViewRenderer : IViewRenderer
	{
		//http://stackoverflow.com/a/4692447/173957
		#region IViewRenderer Members

		public string RenderView(Controller controller, string viewName, object model)
		{
			controller.ViewData.Model = model;
			using (var sw = new StringWriter())
			{
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
				var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
				viewResult.View.Render(viewContext, sw);

				//http://stackoverflow.com/a/5267041/173957
				viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

				return sw.ToString();
			}
		}

		#endregion
	}
}