using CommonDomain.Core;

namespace CliqFlip.Domain.Entities.MajorLocationRoot
{
    public class MajorLocation : AggregateBase
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneOffsetInSeconds { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SlugSpecificSubset { get; set; }
        public string NameSpecificSubset { get; set; }
        public string LatitudeSpecificSubset { get; set; }
        public string LongitudeSpecificSubset { get; set; }
    }
}