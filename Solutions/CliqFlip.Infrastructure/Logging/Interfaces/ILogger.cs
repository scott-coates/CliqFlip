﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.Logging.Interfaces
{
	public interface ILogger
	{
		void LogException(Exception e);
	}
}
