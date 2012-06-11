using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;
using MvcContrib.Pagination;
using CliqFlip.Domain.Dtos;
using System.Linq;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class UsersByInterestViewModel
	{
		public IList<IndividualResultViewModel> Results { get; private set; }
		public IPagination<IndividualResultViewModel> PagedResults { get; set; }
		public UsersByInterestViewModel()
		{
			Results = new List<IndividualResultViewModel>();
		}

		#region Nested type: IndividualResultViewModel

		public class IndividualResultViewModel
		{
            public string Name { get; set; }
			public IList<IndividualResultInterestViewModel> ResultInterestViewModels { get; set; }
			public string Bio { get; set; }
            public string Headline { get; set; }
            public string ImageUrl { get; set; }
            public int NumberOfInterestsWithImages { get; set; }
            public IList<IndividualResultInterestViewModel> InterestsWithImages { get; set; }

			public IndividualResultViewModel()
			{
				ResultInterestViewModels = new List<IndividualResultInterestViewModel>();
			}

            public IndividualResultViewModel(UserSearchByInterestsDto user, List<string> interests)
            {
                ResultInterestViewModels = new List<IndividualResultInterestViewModel>();
                Headline = user.User.Headline;
                Name = user.User.Username;
                Bio = user.User.Bio;
                ImageUrl = user.User.ImageUrl;

                ResultInterestViewModels = user.User.InterestDtos
                    .Select(x => new IndividualResultInterestViewModel
                                    {
                                        InterestName = x.Name,
                                        IsMatch = interests.Contains(x.Slug.ToLower()),
                                        Passion = x.Passion,
                                        DefaultImageUrl = x.DefaultImageUrl
                                    }).OrderByDescending(x => x.IsMatch).ThenByDescending(x => x.Passion).Take(5).ToList();
                
            }


        }

		#endregion

		public class IndividualResultInterestViewModel
		{
			public string InterestName { get; set; }
			public bool IsMatch { get; set; }
            public float? Passion { get; set; }
            public string DefaultImageUrl { get; set; }
        }
	}



}