using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class SpecificInterestGraphViewModel
	{
		public SpecificInterestItemViewModel Interest { get; set; }

		public string RelatedInterestItemViewModelsJson { get; set; }

		[Required]
		public SelectList RelationShipType { get; set; }

		#region Nested type: RelatedSpecificInterestItemViewModel

		public class RelatedSpecificInterestItemViewModel
		{
			public SpecificInterestItemViewModel Interest { get; set; }
			public float Weight { get; set; }
		}

		#endregion

		#region Nested type: SpecificInterestItemViewModel

		public class SpecificInterestItemViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Slug { get; set; }
		}

		#endregion
	}
}