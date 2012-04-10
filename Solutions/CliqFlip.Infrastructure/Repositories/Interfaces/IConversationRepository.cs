using System.Collections.Generic;
using System.Linq;
using Amazon.EC2.Model;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IConversationRepository : IRepository<Conversation>
	{
	}
}