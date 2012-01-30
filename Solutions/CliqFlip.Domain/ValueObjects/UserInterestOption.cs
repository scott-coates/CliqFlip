using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ValueObjects
{
	public class UserInterestOption : ValueObject
	{
		//http://davybrion.com/blog/2009/03/implementing-a-value-object-with-nhibernate/
		private readonly float? _passion;
		private readonly float? _xAxis;
		private readonly float? _yAxis;

		public UserInterestOption(float? passion, float? xAxis, float? yAxis)
		{
			_passion = passion;
			_xAxis = xAxis;
			_yAxis = yAxis;
		}

		private UserInterestOption()
		{
			// the default constructor is only here for NH (private is sufficient, it doesn't need to be public)
		}

		public virtual float? Passion
		{
			get { return _passion; }
		}

		public virtual float? XAxis
		{
			get { return _xAxis; }
		}

		public virtual float? YAxis
		{
			get { return _yAxis; }
		}
	}
}