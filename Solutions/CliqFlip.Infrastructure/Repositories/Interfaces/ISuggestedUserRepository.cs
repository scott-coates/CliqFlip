
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.ReadModels;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
    public interface ISuggestedUserRepository
    {
        // ReSharper disable ReturnTypeCanBeEnumerable.Global
        void Save(User user, IList<UserSearchResultDto> users);
        IQueryable<UserSearchResultDto> GetSuggestedUsers(User user);
        // ReSharper restore ReturnTypeCanBeEnumerable.Global
    }
}