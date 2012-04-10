using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class ConversationRepository :LinqRepository<Conversation>, IConversationRepository
	{
	}
}