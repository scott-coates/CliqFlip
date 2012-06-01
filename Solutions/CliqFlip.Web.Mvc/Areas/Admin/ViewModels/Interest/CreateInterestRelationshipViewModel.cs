using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CliqFlip.Web.Mvc.Validation;
using CliqFlip.Web.Mvc.ViewModels.User;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class CreateInterestRelationshipViewModel
	{
		[Required]
		public int Id { get; set; }

		public float RelationShipType { get; set; }

		[CollectionNotEmpty(ErrorMessage = "Okay, we get it, you're not very interesting. But please, for our sake, just provide us with an interest that tells us about yourself.")]
		public List<InterestCreate> UserInterests { get; set; }

		public CreateInterestRelationshipViewModel()
		{
			UserInterests = new List<InterestCreate>();
		}
	}
}