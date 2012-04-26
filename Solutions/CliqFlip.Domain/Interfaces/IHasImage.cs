using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Interfaces
{
	public interface IHasImage
	{
		ImageData ImageData { get; }
	}
}