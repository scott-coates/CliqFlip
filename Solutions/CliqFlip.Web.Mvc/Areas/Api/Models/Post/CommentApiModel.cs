using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.Areas.Api.Models.Post
{
	public class CommentApiModel
	{
        [Required]
        public string CommentText { get; set; }
        public int PostId { get; set; }
	}
}