using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.User;
using NHibernate.Linq;
using SharpArch.NHibernate;

namespace CliqFlip.Web.Mvc.Queries
{
	public class ConversationQuery : NHibernateQuery, IConversationQuery
	{
		public IEnumerable<MessageViewModel> GetMessages(int conversationId, string username)
		{
            List<MessageViewModel> retVal = null;
            //get the conversation the sender is requesting
            //also make sure the sender is part of this conversations
            Conversation conversation = Session.Query<Conversation>()
                                                .FirstOrDefault(x => x.Id == conversationId &&
                                                    x.Participants.Any(y => y.User.Username == username));

            if (conversation != null)
            {
                retVal = new List<MessageViewModel>();
                IEnumerable<Message> messages = conversation.Messages.Take(10);

                foreach (var message in messages)
                {
                    retVal.Add(new MessageViewModel(message));
                }
                retVal.Reverse();
            }

			return retVal;
		}
	}
}