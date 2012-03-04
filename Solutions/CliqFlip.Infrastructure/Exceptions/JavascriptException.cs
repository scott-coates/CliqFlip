using System;
using System.Runtime.Serialization;

namespace CliqFlip.Infrastructure.Exceptions
{
	public class JavaScriptException : Exception
	{
		public JavaScriptException()
		{
		}

		public JavaScriptException(string message) : base(message)
		{
		}

		public JavaScriptException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected JavaScriptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}