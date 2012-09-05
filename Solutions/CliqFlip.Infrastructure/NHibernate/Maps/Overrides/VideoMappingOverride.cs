using CliqFlip.Domain.ReadModels;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class VideoMappingOverride : IAutoMappingOverride<Video>
	{
		public void Override(AutoMapping<Video> mapping)
		{
			mapping.Table("Videos"); //required because it's a subclass
		}
	}
}
