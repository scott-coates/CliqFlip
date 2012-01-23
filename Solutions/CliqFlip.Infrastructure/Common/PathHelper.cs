using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CliqFlip.Infrastructure.Common
{
	public static class PathHelper
	{
		public static string GetExecutingDirectory()
		{
			//returns /bin in web
			//returns /bin/debub in console
			string path = Assembly.GetExecutingAssembly().GetName().CodeBase;
			return Path.GetDirectoryName(new Uri(path).LocalPath);
		}

		public static string GetExecutingPathOfAssembly(Assembly executingAssembly)
		{
			string path = executingAssembly.GetName().CodeBase;
			return new Uri(path).LocalPath;
		}

		public static string GetExecutingDirectoryOfAssembly(Assembly executingAssembly)
		{
			return Path.GetDirectoryName(GetExecutingPathOfAssembly(executingAssembly));
		}

		public static string GetExecutingDirectory2()
		{
			//returns /root (not bin) in web
			//returns /bin/debug in console
			return AppDomain.CurrentDomain.BaseDirectory;
		}
	}
}
