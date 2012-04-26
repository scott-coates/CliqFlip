using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class MediumMappingOverride: IAutoMappingOverride<Medium>
	{
		public void Override(AutoMapping<Medium> mapping)
		{
			mapping.Map(x => x.InterestMediumOrder).Access.ReadOnly();
		}
	}
}
