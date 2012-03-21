using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class MajorLocation : Entity
	{
		public virtual string Slug { get; set; }
		public virtual string Name { get; set; }
		public virtual string Country { get; set; }
		public virtual string TimeZone { get; set; }
		public virtual string TimeZoneOffsetInSeconds { get; set; }
		public virtual string Latitude { get; set; }
		public virtual string Longitude { get; set; }
		public virtual string SlugSpecificSubset { get; set; }
		public virtual string NameSpecificSubset { get; set; }
		public virtual string Name3 { get; set; }
		public virtual string LatitudeSpecificSubset { get; set; }
		public virtual string LongitudeSpecificSubset { get; set; }
	}
}