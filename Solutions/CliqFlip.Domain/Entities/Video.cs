using CliqFlip.Domain.Interfaces;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Entities
{
	public class Video : Medium, IHasImage
	{
		public virtual string VideoUrl { get; set; }

		public virtual Image Image { get; set; }

		public virtual ImageData ImageData
		{
			get { return Image.ImageData; }
		}
	}
}