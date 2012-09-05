using CliqFlip.Domain.ReadModels;
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

            //don't need to add inverse, cascade, etc as that's taken care of in the convention
            //ex: <set access="field.camelcase-underscore" cascade="all-delete-orphan" inverse="true" name="Posts" order-by="InterestPostOrder">
            mapping.HasMany(x => x.Comments).OrderBy("PostCommentOrder");

            mapping.References(x => x.Medium)
                .Not.LazyLoad() //chances are, we always want the medium associated with a post
                .Cascade.All();
        }
    }
}