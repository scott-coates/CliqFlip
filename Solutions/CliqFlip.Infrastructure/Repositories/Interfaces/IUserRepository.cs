
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
		IQueryable<User> GetUsersByInterests(IList<string> interestAliases);
		User FindByNameOrEmail(string usernameOrEmail );
		User FindByName(string username );

		// ReSharper restore ReturnTypeCanBeEnumerable.Global
	}
}