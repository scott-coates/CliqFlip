namespace CliqFlip.Domain.Entities.Graph
{
	public class GraphInterest
	{
		public string Name { get; set; }
		public int SqlId { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }
		public bool IsMainCategory { get; set; }
	}
}