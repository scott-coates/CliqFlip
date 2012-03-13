using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernateMaps.Overrides
{
	public class ConversationMappingOverride: IAutoMappingOverride<Conversation>
	{
        public void Override(AutoMapping<Conversation> mapping)
		{
            mapping.HasManyToMany(conversation => conversation.Users)
                    .Cascade.All()
                    .Table("UsersConversations")
                    .ParentKeyColumn("ConversationId")
                    .ChildKeyColumn("UserId");
            mapping.HasMany(conv => conv.Messages).OrderBy("SendDate desc");
		}
	}
}
