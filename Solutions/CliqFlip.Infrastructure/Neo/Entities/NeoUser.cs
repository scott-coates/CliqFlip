using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Infrastructure.Neo.Entities
{
	public class NeoUser
	{
        [Range(1, int.MaxValue)]
		public int SqlId { get; set; }		 
	}
}