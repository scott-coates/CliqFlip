using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
	public class Location : Entity
	{
		private LocationData _data;

		public virtual LocationData Data
		{
			//http://stackoverflow.com/a/685026/173957
			get
			{
				return _data ??
				       new LocationData(null, null, null, null, null, 0, 0, null, null, null, null, null);
			}
			set { _data = value; }
		}

		public virtual MajorLocation MajorLocation { get; set; }
	}
}