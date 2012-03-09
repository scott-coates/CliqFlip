using System.Collections.Generic;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class UserInterest : Entity
	{
		private readonly Iesi.Collections.Generic.ISet<Image> _images;
		private UserInterestOption _options;

		public virtual User User { get; set; }
		public virtual Interest Interest { get; set; }
		public virtual int? SocialityPoints { get; set; }

		public virtual UserInterestOption Options
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _options ?? new UserInterestOption(null, null, null); }
			set { _options = value; }
		}

		public virtual IEnumerable<Image> Images
		{
			get { return new List<Image>(_images).AsReadOnly(); }
		}

		public UserInterest()
		{
			_images = new HashedSet<Image>();
		}

		//TODO: consider law of demeter violation
		//http://msdn.microsoft.com/en-us/magazine/cc947917.aspx#id0070040 - i think we can skip the law of demeter since we're working
		//directly with user intersts
		public virtual void AddImage(ImageData data)
		{
			var image = new Image
			{
				Data = data,
				UserInterest = this
			};

			_images.Add(image);
		}

		public virtual void MakeImageDefault(Image image)
		{
			var temp = new List<Image>(_images);
			_images.Clear();
			_images.Add(image);
			temp.Remove(image);
			_images.AddAll(temp);
		}
	}
}