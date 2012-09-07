using System;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
	public class MajorLocation : Entity
	{
        //TODO Put these into event store with migration
		public virtual string Slug { get; set; }
		public virtual string Name { get; set; }
		public virtual string Country { get; set; }
		public virtual string TimeZone { get; set; }
		public virtual string TimeZoneOffsetInSeconds { get; set; }
		public virtual string Latitude { get; set; }
		public virtual string Longitude { get; set; }
		public virtual string SlugSpecificSubset { get; set; }
		public virtual string NameSpecificSubset { get; set; }
		public virtual string LatitudeSpecificSubset { get; set; }
		public virtual string LongitudeSpecificSubset { get; set; }
		public virtual string Guid { get; set; }
	}
}