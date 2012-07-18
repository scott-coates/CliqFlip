using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserSaveMediumViewModel
    {
        [Range(0, int.MaxValue)]
        public int InterestId { get; set; }

        public string Description { get; set; }

        [Required]
        public string FileData { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}