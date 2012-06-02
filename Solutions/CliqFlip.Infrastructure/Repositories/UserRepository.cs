using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Migrator.Migrations;
using CliqFlip.Infrastructure.NHibernate.Extensions;
using CliqFlip.Infrastructure.Neo.NodeTypes;
using CliqFlip.Infrastructure.Neo.Relationships;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using NHibernate;
using Neo4jClient;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class UserRepository : LinqRepository<User>, IUserRepository
	{
		//TODO: All data logic in the user tasks should be moved here

		private readonly IGraphClient _graphClient;

		public UserRepository(IGraphClient graphClient)
		{
			_graphClient = graphClient;
		}

		#region IUserRepository Members

		public User GetSuggestedUser(User user)
		{
			User retVal;
			int rand = new Random().Next(100);
			bool onlyLocal = rand <= 75;

			IQueryOver<User, User> query = Session.QueryOver<User>()
				.Where(x => x.Id != user.Id);

			if (onlyLocal)
			{
				retVal = query
					.JoinQueryOver(x => x.Location)
					.Where(x => x.MajorLocation.Id == user.Location.MajorLocation.Id)
					.OrderByRandom()
					.Take(1)
					.SingleOrDefault();

				if (retVal == null)
				{
					retVal = Session.QueryOver<User>()
						.Where(x => x.Id != user.Id)
						.OrderByRandom()
						.Take(1)
						.SingleOrDefault();
				}
			}
			else
			{
				retVal = query
					.OrderByRandom()
					.Take(1)
					.SingleOrDefault();
			}

			return retVal ?? user;
		}

		public IQueryable<User> GetUsersByInterests(IList<string> interestAliases)
		{
			//TODO we have code in the userInterest Repo that is very similar - try to clean it up
			var query = new AdHoc<User>(x => x.Interests.Any(y => interestAliases.Contains(y.Interest.Slug))
			                                 ||
			                                 x.Interests.Any(y => interestAliases.Contains(y.Interest.ParentInterest.Slug)));

			return FindAll(query);
		}

		public User FindByNameOrEmail(string usernameOrEmail)
		{
			var withMatchingNameOrEmail = new AdHoc<User>(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);

			return FindOne(withMatchingNameOrEmail);
		}

		public User FindByName(string username)
		{
			var adhoc = new AdHoc<User>(x => x.Username == username);
			return FindOne(adhoc);
		}

        public bool IsUsernameOrEmailAvailable(string usernameOrEmail)
        {
            var withMatchingNameOrEmail = new AdHoc<User>(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);
            return !FindAll(withMatchingNameOrEmail).Any();
        }

		public override User SaveOrUpdate(User entity)
		{
			var retVal = base.SaveOrUpdate(entity);

			var neoUser = new NeoUser{SqlId = retVal.Id};

			var ixEntry = new IndexEntry
			{
				Name = "users",
				KeyValues = new[]
						{
							new KeyValuePair<string, object>("sqlid", neoUser.SqlId)
						}
			};

			_graphClient.Create(neoUser, new[] { new UserBelongsTo(_graphClient.RootNode), }, new[] { ixEntry });

			return retVal;
		}

		#endregion
	}
}