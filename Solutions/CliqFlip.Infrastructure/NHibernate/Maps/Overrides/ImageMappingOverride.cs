using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class ImageMappingOverride: IAutoMappingOverride<Image>
	{
		public void Override(AutoMapping<Image> mapping)
		{
			mapping.Map(x => x.InterestImageOrder).Access.ReadOnly();
		}
	}
}
