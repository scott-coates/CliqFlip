using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Overrides
{
	public class ConversationMappingOverride: IAutoMappingOverride<Conversation>
	{
        public void Override(AutoMapping<Conversation> mapping)
		{
            mapping.HasManyToMany(conversation => conversation.Users)
                    .Cascade.All()
                    .Table("UserConversations")
                    .ParentKeyColumn("ConversationId")
                    .ChildKeyColumn("UserId");
            mapping.HasMany(conv => conv.Messages).OrderBy("SendDate desc");
		}
	}
}
