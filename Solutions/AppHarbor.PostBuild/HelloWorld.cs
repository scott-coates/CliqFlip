using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace CustomTask
{
	public class HelloWorld : Task
	{
		public override bool Execute()
		{
			Log.LogMessage(MessageImportance.High, "Inside Custom Task.");
			return true;
		}

		[Output]
		public string Test
		{
			get
			{
				return "Hello, World!";
			}
		}
	}
}
