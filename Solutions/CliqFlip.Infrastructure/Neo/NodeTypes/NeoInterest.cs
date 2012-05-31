namespace CliqFlip.Infrastructure.Neo.NodeTypes
{
	public class NeoInterest
	{
		public string Name { get; set; }
		public int SqlId { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }
		public bool IsMainCategory { get; set; }
	}
}