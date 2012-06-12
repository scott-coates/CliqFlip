using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;
using MvcContrib.Pagination;
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

            public IndividualResultViewModel(UserSearchResultDto user, List<string> interests)
            {
                ResultInterestViewModels = new List<IndividualResultInterestViewModel>();
                Headline = user.Headline;
                Name = user.Username;
                Bio = user.Bio;
                ImageUrl = user.ImageUrl;

                ResultInterestViewModels = user.InterestDtos
                    .Select(
                        x => new IndividualResultInterestViewModel
                        {
                            InterestName = x.Name,
                            //assume interests is ordered smallest to highets
                            MatchScore = interests.IndexOf(x.Slug.ToLower()), 
                            Passion = x.Passion,
                            DefaultImageUrl = x.DefaultImageUrl
                        }).OrderByDescending(x => x.MatchScore).ThenByDescending(x => x.Passion).Take(5).ToList();
            }
        }

        #endregion

        public class IndividualResultInterestViewModel
        {
            public string InterestName { get; set; }
            public int MatchScore { get; set; }
            public float? Passion { get; set; }
            public string DefaultImageUrl { get; set; }
        }
    }
}