﻿namespace CliqFlip.Domain.Dtos.User
{
	public class UserProfileIndexInterestDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int? RelatedTo { get; set; }
		public int? Sociality { get; set; }
		public float? Passion { get; set; }
		public float? XAxis { get; set; }
		public float? YAxis { get; set; }

		public UserProfileIndexInterestDto(int id, string name, string slug, int? relatedTo, int? sociality, float? passion, float? xAxis, float? yAxis)
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
    }
}