using CliqFlip.Domain.Entities;
using System.Linq;

namespace CliqFlip.Domain.Dtos
{
	public class UserInterestDto
	{
        //TODO: don't reuse this dto - make copies for specific purposes
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int? RelatedTo { get; set; }
		public int? Sociality { get; set; }
		public float? Passion { get; set; }
		public float? XAxis { get; set; }
		public float? YAxis { get; set; }
        public string DefaultImageUrl { get; set; }

		public UserInterestDto(int id, string name, int? relatedTo)
		{
			Id = id;
			Name = name;
			RelatedTo = relatedTo;
		}

		public UserInterestDto(int id, string name, string slug, int? relatedTo, int? sociality, float? passion, float? xAxis, float? yAxis)
		{
			Id = id;
			Name = name;
			Slug = slug;
			RelatedTo = relatedTo;
			Sociality = sociality;
			Passion = passion;
			XAxis = xAxis;
			YAxis = yAxis;
		}

        public UserInterestDto(UserInterest interests)
        {
            Id = interests.Id;
            Name = interests.Interest.Name;
            Slug = interests.Interest.Slug;
            Passion = interests.Options.Passion;
            var defaultImage = interests.Media.FirstOrDefault() as Image;
            DefaultImageUrl = defaultImage != null ? defaultImage.ImageData.MediumFileName : null;
        }
    }
}