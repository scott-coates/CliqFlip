using CliqFlip.Domain.Interfaces;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Entities
{
	public class WebPage : Medium, IHasImage
	{
		public virtual string LinkUrl { get; set; }

		public virtual string Title { get; set; }

		public virtual string WebPageDomainName { get; set; }

		public virtual Image Image { get; set; }

		public virtual ImageData ImageData
		{
			get { return Image.ImageData; }
		}

		public virtual void AddImage(ImageData data)
		{
			Image = new Image {ImageData = data};
		}
	}
}