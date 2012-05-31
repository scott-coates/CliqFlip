using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class InterestSearchViewModel
	{
		[Required]
		public string SearchTearm { get; set; } 
	}
}