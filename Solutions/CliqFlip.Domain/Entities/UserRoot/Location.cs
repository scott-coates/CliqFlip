using System;

namespace CliqFlip.Domain.Entities.UserRoot
{
    public class Location
    {
        public Guid MajorLocationId { get; set; }
        public string LocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Location(Guid majorLocationId, string locationName, float latitude, float longitude)
        {
            MajorLocationId = majorLocationId;
            LocationName = locationName;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}