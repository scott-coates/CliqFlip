using System;
using CliqFlip.Infrastructure.Logging.Interfaces;
using Elmah;

namespace CliqFlip.Infrastructure.Logging
{
	public class ElmahLogger : ILogger
	{
		public void LogException(Exception e)
		{
			ErrorSignal.FromCurrentContext().Raise(e);
		}
	}
}