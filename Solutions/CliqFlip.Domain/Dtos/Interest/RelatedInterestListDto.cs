using System.Collections.Generic;

namespace CliqFlip.Domain.Dtos.Interest
{
	public class RelatedInterestListDto
	{
		public RelatedInterestDto OriginalInterest { get; set; }
		public List<WeightedRelatedInterestDto> WeightedRelatedInterestDtos { get; set; }
		#region Nested type: RelatedInterestDto

		public class RelatedInterestDto
		{
		    public RelatedInterestDto(int id, int? parentId, string name, string slug)
		    {
		        Id = id;
		        ParentId = parentId;
		        Name = name;
		        Slug = slug;
		    }

		    public int Id { get; set; }
			public int? ParentId { get; set; }
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