namespace CliqFlip.Infrastructure.Common
{
	public static class AssemblyHelper
	{
		private readonly static string _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public static string GeneratePath(string fileName)
		{
			return string.Format("{0}?v={1}", fileName, _version);
		}
	}
}