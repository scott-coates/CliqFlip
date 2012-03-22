using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class UserInterestMappingOverride: IAutoMappingOverride<UserInterest>
	{
		public void Override(AutoMapping<UserInterest> mapping)
		{
			//don't need to add inverse, cascade, etc as that's taken care of in the convention
			mapping.HasMany(x => x.Images).OrderBy("InterestImageOrder");
		}
	}
}
