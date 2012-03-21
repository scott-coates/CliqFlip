using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class Location : Entity
	{
		public virtual string CountryCode { get; set; }
		public virtual string CountryName { get; set; }
		public virtual string RegionCode { get; set; }
		public virtual string RegionName { get; set; }
		public virtual string MetroCode { get; set; }
		public virtual string County { get; set; }
		public virtual string Street { get; set; }
		public virtual string City { get; set; }
		public virtual string ZipCode { get; set; }
		public virtual string AreaCode { get; set; }
		public virtual float Latitude { get; set; }
		public virtual float Longitude { get; set; }
		public virtual MajorLocation MajorLocation { get; set; }
	}
}