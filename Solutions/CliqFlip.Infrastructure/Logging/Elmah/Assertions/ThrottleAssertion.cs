#region License, Terms and Author(s)
//
// ELMAH - Error Logging Modules and Handlers for ASP.NET
// Copyright (c) 2004-11 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Jesse Arnold
//      Scott Coates
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Diagnostics;
using Elmah;
using Elmah.Assertions;

namespace CliqFlip.Infrastructure.Logging.Elmah.Assertions
{
	public class ThrottleAssertion : IAssertion
	{
		private readonly TimeSpan _throttleDelay;
		private readonly bool _traceThrottledExceptions;

		private Exception _previousException;
		private DateTime _timeOfLastUnfilteredException;

		public ThrottleAssertion()
			: this(new TimeSpan(), false)
		{
		}

		public ThrottleAssertion(TimeSpan delay, bool traceThrottledExceptions)
		{
			_throttleDelay = delay;
			_traceThrottledExceptions = traceThrottledExceptions;
		}

		#region IAssertion Members

		public bool Test(object context)
		{
			if (context == null) throw new ArgumentNullException("context");

			Exception currentException = ((ErrorFilterModule.AssertionHelperContext) context).BaseException;

			bool testResult = false;

			if (_previousException != null)
			{
				bool match = TestExceptionMatch(currentException);

				bool throttled = false; //throttle means skip

				if (match)
				{
					// If the throttle delay is not specified, this will throttle all repeated exceptions.
					// Otherwise, check to see if the elapsed time exceeds the throttle delay to determine
					// if the exception should be filtered.

					throttled = _throttleDelay.TotalMilliseconds <= 0 || DateTime.UtcNow.Subtract(_timeOfLastUnfilteredException) <= _throttleDelay;
				}

				testResult = match && throttled;
			}

			_previousException = currentException;

			// reset throttle delay timer
			if (!testResult)
			{
				_timeOfLastUnfilteredException = DateTime.UtcNow;
			}

			Trace.WriteIf(_traceThrottledExceptions && testResult, currentException);

			return testResult;
		}

		#endregion

		protected virtual bool TestExceptionMatch(Exception currentException)
		{
			return (
			       	currentException.Message == _previousException.Message &&
			       	currentException.Source == _previousException.Source &&
			       	currentException.TargetSite == _previousException.TargetSite
			       );
		}
	}
}