using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserSavePostViewModel
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