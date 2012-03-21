using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ValueObjects
{
	public class LocationData : ValueObject
	{
		//http://davybrion.com/blog/2009/03/implementing-a-value-object-with-nhibernate/
		private readonly string _areaCode;
		private readonly string _city;
		private readonly string _countryCode;
		private readonly string _countryName;
		private readonly string _county;
		private readonly float _latitude;
		private readonly float _longitude;
		private readonly string _metroCode;
		private readonly string _regionCode;
		private readonly string _regionName;
		private readonly string _street;
		private readonly string _zipCode;

		public virtual string CountryCode
		{
			get { return _countryCode; }
		}

		public virtual string CountryName
		{
			get { return _countryName; }
		}

		public virtual string RegionCode
		{
			get { return _regionCode; }
		}

		public virtual string RegionName
		{
			get { return _regionName; }
		}

		public virtual string MetroCode
		{
			get { return _metroCode; }
		}

		public virtual string County
		{
			get { return _county; }
		}

		public virtual string Street
		{
			get { return _street; }
		}

		public virtual string City
		{
			get { return _city; }
		}

		public virtual string ZipCode
		{
			get { return _zipCode; }
		}

		public virtual string AreaCode
		{
			get { return _areaCode; }
		}

		public virtual float Latitude
		{
			get { return _latitude; }
		}

		public virtual float Longitude
		{
			get { return _longitude; }
		}

		public LocationData(string areaCode, string city, string countryCode, string countryName, string county, float latitude, float longitude, string metroCode, string regionCode, string regionName, string street, string zipCode)
		{
			_areaCode = areaCode;
			_city = city;
			_countryCode = countryCode;
			_countryName = countryName;
			_county = county;
			_latitude = latitude;
			_longitude = longitude;
			_metroCode = metroCode;
			_regionCode = regionCode;
			_regionName = regionName;
			_street = street;
			_zipCode = zipCode;
		}

		private LocationData()
		{
			// the default constructor is only here for NH (private readonly is sufficient, it doesn't need to be public)
		}
	}
}