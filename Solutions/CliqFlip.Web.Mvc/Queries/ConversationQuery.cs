using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.User;
using NHibernate.Linq;
using Newtonsoft.Json;
using SharpArch.NHibernate;
using CliqFlip.Web.Mvc.ViewModels;

namespace CliqFlip.Web.Mvc.Queries
{
	public class ConversationQuery : NHibernateQuery, IConversationQuery
	{
		
		public IEnumerable<MessageViewModel> GetUserProfileIndex(int conversationId, string username)
		{
            List<MessageViewModel> retVal = null;
            Conversation conversation = Session.Query<Conversation>()
                                                .FirstOrDefault(x => x.Id == conversationId &&
                                                    x.Participants.Any(y => y.User.Username == username));

            if (conversation != null)
            {
                retVal = new List<MessageViewModel>();
                var messages = conversation.Messages.OrderByDescending(message => message.SendDate).Take(10);

                foreach (var message in messages)
                {
                    var mess = new MessageViewModel
                    {
                        Text = message.Text,
                        SendDate = message.SendDate,
                        Sender = message.Sender.Username,
                        SenderImageUrl = message.Sender.ProfileImageData.ThumbFileName
                    };
                    retVal.Add(mess);
                }
                retVal.Reverse();
            }

			return retVal;
		}
	}
}