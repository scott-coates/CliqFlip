using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernateMaps.Overrides
{
	public class UserOverride : IAutoMappingOverride<User>
	{
		public void Override(AutoMapping<User> mapping)
		{
            mapping.HasMany(x => x.Interests);
		}
	}
}