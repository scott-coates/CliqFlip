using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.Extensions.Controller
{
	public static class ControllerExtensions
	{
		//http://stackoverflow.com/questions/5581214/flash-equivalent-in-asp-net-mvc-3
		public static void FlashSuccess(this System.Web.Mvc.Controller controller, string message)
		{
			controller.TempData[Constants.SUCCESS_TEMPDATA_KEY] = message;
		}

		public static void FlashError(this System.Web.Mvc.Controller controller, string message)
		{
			controller.TempData[Constants.ERROR_TEMPDATA_KEY] = message;
		}
	}
}