using CliqFlip.Domain.ReadModels;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class ImageMappingOverride : IAutoMappingOverride<Image>
	{
		public void Override(AutoMapping<Image> mapping)
		{
            mapping.Table("Images"); //required because it's a subclass
		}
	}
}
