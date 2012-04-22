using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Views.Interfaces
{
	public interface IViewRenderer
	{
		string RenderView(Controller controller, string viewName, object model);
	}
}
