namespace CliqFlip.Web.Mvc.ViewModels.Jeip
{
	public class JeipSaveResponseViewModel
	{
		/*
		 * http://josephscott.org/code/javascript/jquery-edit-in-place/
			is_error – (bool) When true indicates that there was some sort of error when the save URL processed the XHR. Under normal conditions this should be set to false.
			error_text – (string) When is_error is true, this string is used to provide a plain text descriptive of what the error was.
			html – (string) This string is used to replace the old value of the edited DOM element.
		 */

		public bool is_error { get; set; }
		public string error_text { get; set; }
		public string html { get; set; }
	}
}