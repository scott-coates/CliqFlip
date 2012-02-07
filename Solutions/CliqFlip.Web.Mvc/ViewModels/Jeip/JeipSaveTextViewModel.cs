namespace CliqFlip.Web.Mvc.ViewModels.Jeip
{
	public class JeipSaveTextViewModel
	{
		/*
		 * http://josephscott.org/code/javascript/jquery-edit-in-place/
			url – (string) URL that the edit form was on.
			form_type – (string) The type of edit form that was used (text, textarea, select)
			id – (string) The id of the DOM element that was edited.
			orig_value – (string) The original value of the DOM element that was edited.
			new_value – (string) The new value of the DOM element that was edited.
			data – (object) Optional additional data that was passed along via the original eip call.
		 */

		public string Url { get; set; }
		public string Form_Type { get; set; }
		public string Id { get; set; }
		public string Orig_Value { get; set; }
		public string New_Value { get; set; }
		public string Data { get; set; }
	}
}