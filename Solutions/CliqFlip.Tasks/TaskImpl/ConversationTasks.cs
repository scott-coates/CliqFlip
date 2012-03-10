using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Search;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;
using System.Security.Principal;

namespace CliqFlip.Tasks.TaskImpl
{
	public class ConversationTasks : IConversationTasks
	{
        private readonly ILinqRepository<Conversation> _conversationRepository;
		//userInterestRepo is aggregate root - http://stackoverflow.com/a/5806356/173957
		private readonly ILinqRepository<User> _userInterestRepository;

        public ConversationTasks(ILinqRepository<User> userInterestRepository, ILinqRepository<Conversation> conversationRepository)
		{
            _conversationRepository = conversationRepository;
			_userInterestRepository = userInterestRepository;
		}

		#region IInterestTasks Members

        public bool SendMessage(String from, String to, String text)
        {
            //get the user logged in
            var currentUserQuery = new AdHoc<User>(x => x.Username == from);
            var sender = _userInterestRepository.FindOne(currentUserQuery);

            var recipientQuery = new AdHoc<User>(x => x.Username == to);
            var recipient = _userInterestRepository.FindOne(recipientQuery);

            //see if the recipient has any active conversations with the sender

            //get all the conversations where the recipient is an active participant
            var conversations = recipient.Participants.Where(x => x.IsActive).Select(x => x.Conversation).ToList();

            //see if the recipient has any active conversations with the sender
            var conversation = conversations.SingleOrDefault(x => x.Participants.Any(y => y.User == sender));

            if (conversation == null)
            {
                conversation = new Conversation(sender, recipient);
            }

            //sender.WriteMessage(text)
            var message = new Message(sender, text);
            message.Conversation = conversation;
            conversation.AddMessage(message);
            _conversationRepository.Save(conversation);

            return true;
        }

        public Message Reply(string from, int conversationId, string text)
        {
            Message retVal = null;
            var senderUserQuery = new AdHoc<User>(x => x.Username == from);
            var user = _userInterestRepository.FindOne(senderUserQuery);

            if (user != null)
            {
                var conversation = user.Participants.SingleOrDefault(x => x.Conversation.Id == conversationId).Conversation;

                if (conversation != null)
                {
                    //AdHoc<Conversation> conversationQuery = new AdHoc<Conversation>(conv => conv.Id == conversationId && conv.Participants.Any(part => part.User.Username == from));
                    //var conversation = _conversationRepository.FindOne(conversationQuery);

                    retVal = new Message(user, text);
                    //retVal.Conversation = conversation;
                    conversation.AddMessage(retVal);
                }
            }
            return retVal;
        }
		#endregion
    }
}