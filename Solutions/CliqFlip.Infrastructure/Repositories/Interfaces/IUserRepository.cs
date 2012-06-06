
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		// ReSharper disable ReturnTypeCanBeEnumerable.Global

		User GetSuggestedUser(User user);
		IQueryable<User> GetUsersByInterests(IList<int> interestIds);
		User FindByNameOrEmail(string usernameOrEmail );
		User FindByName(string username );
        bool IsUsernameOrEmailAvailable(string usernameOrEmail);
		// ReSharper restore ReturnTypeCanBeEnumerable.Global
	}
}