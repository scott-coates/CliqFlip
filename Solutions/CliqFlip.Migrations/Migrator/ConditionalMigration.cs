using System;
using System.Configuration;
using System.Reflection;
using Migrator.Framework;

namespace CliqFlip.Migrations.Migrator
{
	public abstract class ConditionalMigration : Migration
	{
		private static readonly bool _allowTestData;

		static ConditionalMigration()
		{
			string path = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase + ".config").LocalPath;

			var map = new ExeConfigurationFileMap {ExeConfigFilename = path};

			Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			bool.TryParse(cfg.AppSettings.Settings["AllowTestMigration"].Value, out _allowTestData);

			Console.WriteLine("AllowTestMigration = " + _allowTestData);
		}

		protected abstract void ConditionalUp();
		protected abstract void ConditionalDown();

		public sealed override void Up()
		{
			if (_allowTestData)
			{
				ConditionalUp();
			}
		}

		public sealed override void Down()
		{
			if (_allowTestData)
			{
				ConditionalDown();
			}
		}
	}
}