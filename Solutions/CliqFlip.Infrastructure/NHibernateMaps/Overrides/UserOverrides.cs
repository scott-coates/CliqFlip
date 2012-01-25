using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernateMaps.Overrides
{
    public class UserOverrides : IAutoMappingOverride<User>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<User> mapping)
        {
            mapping.HasMany(c => c.Interests).Table("UserInterests").KeyColumn("UserId").Cascade.All().AsBag().Inverse();
        }
    }
}
