﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.Web.Interfaces
{
	public interface IHtmlService
	{
		string GetHtmlFromUrl(string url);
	}
}
