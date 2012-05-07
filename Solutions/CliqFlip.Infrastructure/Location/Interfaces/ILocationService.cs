using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Infrastructure.Location.Interfaces
{
	public interface ILocationService
	{
		LocationData GetLocation(string input);
		LocationData ParseLocationData(string locationData);
		MajorLocation GetNearestMajorCity(float latitude, float longitude);
	}
}