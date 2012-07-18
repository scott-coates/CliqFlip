using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserSaveMediumViewModel
    {
        [Range(0, int.MaxValue)]
        public int InterestId { get; set; }

        public string Description { get; set; }

        [Required]
        public string MediumData { get; set; }

        [Required]
        public string MediumType { get; set; }

        public string FileName { get; set; }
    }
}