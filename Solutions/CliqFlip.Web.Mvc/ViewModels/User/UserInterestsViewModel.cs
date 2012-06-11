using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Media;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserInterestsViewModel : UserProfileViewModel
	{
		public IList<InterestViewModel> Interests { get; set; }
		public string MakeDefaultUrl { get; set; }
		public string RemoveImageUrl { get; set; }

		public UserInterestsViewModel()
		{
			Interests = new List<InterestViewModel>();
		}

		#region Nested type: TagCloudInterestsViewModel

		public class InterestViewModel
		{
			public string Name { get; set; }
			public int InterestId { get; set; }
			public int UserInterestId { get; set; }
			public bool VisitorSharesThisInterest { get; set; }
			public IList<UserInterestMediumViewModel> Media { get; set; }

			public InterestViewModel()
			{
				Media = new List<UserInterestMediumViewModel>();
			}
		}

        public class UserInterestMediumViewModel : InterestMediumViewModel
        {
        	public UserInterestMediumViewModel(MediumDto medium) : base(medium){}
        }

		#endregion
	}
}