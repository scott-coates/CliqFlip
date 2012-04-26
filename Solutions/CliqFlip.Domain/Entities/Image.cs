using CliqFlip.Domain.Interfaces;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Entities
{
	public class Image : Medium, IHasImage
	{
		private ImageData _imageData;

		public virtual ImageData ImageData
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _imageData ?? new ImageData(null, null, null, null); }
			set { _imageData = value; }
		}
	}
}