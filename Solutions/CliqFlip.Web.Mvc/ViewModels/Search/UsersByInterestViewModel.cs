using System.Collections.Generic;
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
                Headline = user.UserDto.Headline;
                Name = user.UserDto.Username;
                Bio = user.UserDto.Bio;
                ImageUrl = user.UserDto.ImageUrl;
                
                ResultInterestViewModels = user.UserDto.InterestDtos
                    .Select(x => new IndividualResultInterestViewModel
                                    {
                                        InterestName = x.Name,
                                        IsMatch = interests.Contains(x.Slug.ToLower()),
                                        Passion = x.Passion,
                                        DefaultImageUrl = x.DefaultImageUrl
                                    }).OrderByDescending(x => x.IsMatch).ThenByDescending(x => x.Passion).Take(5).ToList();
                InterestsWithImages = ResultInterestViewModels.Where(x => x.DefaultImageUrl != null).ToList(); //generate a list of the interests with images
                
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