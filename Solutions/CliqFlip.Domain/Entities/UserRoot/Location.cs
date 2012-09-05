using CliqFlip.Domain.Entities.MajorLocationRoot;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Entities.UserRoot
{
	public class Location
	{
		private LocationData _data;

		public LocationData Data
		{
			get
			{
				return _data ??
				       new LocationData(null, null, null, null, null, 0, 0, null, null, null, null, null);
			}
			set { _data = value; }
		}

		public MajorLocation MajorLocation { get; set; }
	}
}