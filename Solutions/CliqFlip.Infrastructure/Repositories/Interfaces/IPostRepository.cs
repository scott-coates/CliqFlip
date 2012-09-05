using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.ReadModels;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> GetPostsByInterestTypes(IList<Interest> interests);
    }
}