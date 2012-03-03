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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Elmah;
using Elmah.Assertions;

namespace CliqFlip.Infrastructure.Logging.Elmah.Assertions
{
	public class ThrottleAssertion : IAssertion
	{
		private readonly TimeSpan _throttleDelay;
		private readonly bool _traceThrottledExceptions;
		private readonly Dictionary<string, DateTime> _previousExceptions = new Dictionary<string, DateTime>(); 

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

			var assertionHelperContext = (ErrorFilterModule.AssertionHelperContext) context;

			Exception currentException = assertionHelperContext.BaseException;

			string key = string.Format("{0}-{1}", assertionHelperContext.FilterSourceType.Name, currentException);

			//TODO: this is not thread safe - use concurrect dictionary or memcahed

			bool match = _previousExceptions.ContainsKey(key);

			bool throttled = false; //throttle means skip

			if (match)
			{
				// If the throttle delay is not specified, this will throttle all repeated exceptions.
				// Otherwise, check to see if the elapsed time exceeds the throttle delay to determine
				// if the exception should be filtered.

				throttled = _throttleDelay.TotalMilliseconds <= 0 || DateTime.UtcNow.Subtract(_previousExceptions[key]) <= _throttleDelay;
			}

			bool testResult = match && throttled;

			// reset throttle delay timer
			if (!testResult)
			{
				_previousExceptions[key] = DateTime.UtcNow;
			}

			Trace.WriteIf(_traceThrottledExceptions && testResult, currentException);

			return testResult;
		}

		#endregion

	}
}