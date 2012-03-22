
using CliqFlip.Domain.Entities;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User GetSuggestedUser(User user);
	}
}