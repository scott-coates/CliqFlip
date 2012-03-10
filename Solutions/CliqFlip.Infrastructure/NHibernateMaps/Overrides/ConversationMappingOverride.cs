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
			//don't need to add inverse, cascade, etc as that's taken care of in the convention
			//mapping.HasMany(x => x.Messages);//.ForeignKeyConstraintName("MessageId");
            //mapping.HasMany(x => x.Messages).OrderBy("SendDate").
		}
	}
}
