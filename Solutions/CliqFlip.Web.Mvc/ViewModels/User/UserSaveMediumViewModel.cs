using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserSaveMediumViewModel
	{
        public int InterestId { get; set; }
		public string Description { get; set; }
        public string FileData { get; set; }
        public string FileName { get; set; }
		public HttpPostedFileBase InterestImage { get; set; }
	}
}