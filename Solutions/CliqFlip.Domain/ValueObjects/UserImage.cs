using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ValueObjects
{
	public class UserImage : ValueObject
	{
		//http://davybrion.com/blog/2009/03/implementing-a-value-object-with-nhibernate/
		private readonly string _originalFileName;
		private readonly string _thumbFileName;
		private readonly string _mediumFileName;
		private readonly string _fullFileName;

		private UserImage()
		{
			// the default constructor is only here for NH (private is sufficient, it doesn't need to be public)
		}

		public UserImage(string originalFileName, string thumbFileName, string mediumFileName, string fullFileName)
		{
			_originalFileName = originalFileName;
			_thumbFileName = thumbFileName;
			_mediumFileName = mediumFileName;
			_fullFileName = fullFileName;
		}

		public string FullFileName
		{
			get { return _fullFileName; }
		}

		public string MediumFileName
		{
			get { return _mediumFileName; }
		}

		public string ThumbFileName
		{
			get { return _thumbFileName; }
		}

		public string OriginalFileName
		{
			get { return _originalFileName; }
		}
	}
}