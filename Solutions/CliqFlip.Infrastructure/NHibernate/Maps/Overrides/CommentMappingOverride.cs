using CliqFlip.Domain.ReadModels;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
    public class CommentMappingOverride : IAutoMappingOverride<Comment>
    {
        public void Override(AutoMapping<Comment> mapping)
        {
            mapping.Map(x => x.PostCommentOrder).Access.ReadOnly();
        }
    }
}