
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		User GetSuggestedUser(User user);
	}
}