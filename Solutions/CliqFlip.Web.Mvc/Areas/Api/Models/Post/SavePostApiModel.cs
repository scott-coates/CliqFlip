using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.Areas.Api.Models.Post
{
    public class SavePostApiModel
    {
        [Range(0, int.MaxValue)]
        public int InterestId { get; set; }

        public string Description { get; set; }

        [Required]
        public string PostData { get; set; }

        [Required]
        public string PostType { get; set; }

        public string FileName { get; set; }
    }
}