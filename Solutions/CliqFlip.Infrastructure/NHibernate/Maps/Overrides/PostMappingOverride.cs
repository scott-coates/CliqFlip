using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
    public class PostMappingOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.Map(x => x.InterestPostOrder).Access.ReadOnly();

            mapping.References(x => x.Medium)
                .Not.LazyLoad() //chances are, we always want the medium associated with a post
                .Cascade.All();
        }
    }
}