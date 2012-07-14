using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class FeedPostActivityOverviewViewModel
	{
        [Required]
        public string CommentText { get; set; }
        public int PostId { get; set; }
	}
}