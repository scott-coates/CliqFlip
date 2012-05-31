using System.Collections.Generic;

namespace CliqFlip.Domain.Dtos
{
	public class RelatedInterestListDto
	{
		public RelatedInterestDto OriginalInterest { get; set; }
		public List<WeightedRelatedInterestDto> WeightedRelatedInterestDtos { get; set; }
		#region Nested type: RelatedInterestDto

		public class RelatedInterestDto
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Slug { get; set; }
		}

		#endregion

		#region Nested type: WeightedRelatedInterestDto

		public class WeightedRelatedInterestDto
		{
			public RelatedInterestDto Interest { get; set; }
			public float Weight { get; set; }
		}

		#endregion
	}
}