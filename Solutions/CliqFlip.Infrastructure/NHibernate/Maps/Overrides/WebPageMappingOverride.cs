using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
    public class WebPageMappingOverride : IAutoMappingOverride<WebPage>
    {
        public void Override(AutoMapping<WebPage> mapping)
        {
            mapping.Table("WebPages"); //required because it's a subclass
        }
    }
}