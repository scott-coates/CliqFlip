using System;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class InterestCreate
	{
		public int Id { get; set; }
		public String Name { get; set; }
		public int? CategoryId { get; set; }
	}
}