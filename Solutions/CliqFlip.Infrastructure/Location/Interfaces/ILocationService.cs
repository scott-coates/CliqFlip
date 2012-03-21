using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Infrastructure.Location.Interfaces
{
	public interface ILocationService
	{
		LocationData GetLocation(string street = null, string city = null, string state = null, string zip = null);
		LocationData ParseLocationData(string locationData);
	}
}