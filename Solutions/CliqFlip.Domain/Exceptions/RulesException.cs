using System;
using System.Collections.Generic;

namespace CliqFlip.Domain.Exceptions
{
	public class RulesException : Exception
	{
		public IEnumerable<ErrorInfo> Errors { get; private set; }

		public RulesException(IEnumerable<ErrorInfo> errors)
		{
			Errors = errors;
		}

		public RulesException(string propertyName, string errorMessage)
			: this(propertyName, errorMessage, null)
		{
		}

		public RulesException(string propertyName, string errorMessage, object onObject)
		{
			Errors = new[] {new ErrorInfo(propertyName, errorMessage, onObject)};
		}
	}
}