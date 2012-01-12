using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UsersByInterestsQuery : IUsersByInterestsQuery
	{
		private readonly IUserTasks _userTasks;

		public UsersByInterestsQuery(IUserTasks userTasks)
		{
			_userTasks = userTasks;
		}

		#region IUsersByInterestsQuery Members

		public UsersByInterestViewModel GetGetUsersByInterests(string systemAliases)
		{
			List<string> aliasCollection = systemAliases.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Where(x => !x.StartsWith("-1")).ToList();

			IList<UserSearchByInterestsDto> users = _userTasks.GetUsersByInterestsDtos(aliasCollection);
			var retVal = new UsersByInterestViewModel();
			foreach (UserSearchByInterestsDto user in users)
			{
				retVal.Results.Add(new UsersByInterestViewModel.IndividualResultViewModel
				                   	{
				                   		Name = user.UserDto.Username,
				                   		Bio = user.UserDto.Bio,
				                   		ResultInterestViewModels = user.UserDto.InterestDtos
				                   			.Select(x => new UsersByInterestViewModel.IndividualResultInterestViewModel
				                   			             	{
				                   			             		InterestName = x.Name,
				                   			             		IsMatch = aliasCollection.Contains(x.SystemAlias)
				                   			             	}).OrderByDescending(x => x.IsMatch).Take(5).ToList()
				                   	});
			}
			return retVal;
		}

		#endregion
	}
}