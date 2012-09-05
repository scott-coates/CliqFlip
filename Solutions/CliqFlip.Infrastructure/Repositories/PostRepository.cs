using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
    public class PostRepository : LinqRepository<Post>, IPostRepository
    {
        public IQueryable<Post> GetPostsByInterestTypes(IList<Interest> interests)
        {
            var query = new AdHoc<Post>(
                x => interests.Contains(x.Interest)
                     ||
                     interests.Contains(x.Interest.ParentInterest));

            return FindAll(query);
        }
    }
}