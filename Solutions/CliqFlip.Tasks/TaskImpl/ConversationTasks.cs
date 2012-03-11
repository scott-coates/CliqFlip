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
		private readonly ILinqRepository<User> _userRepository;

        public ConversationTasks(ILinqRepository<User> userInterestRepository, ILinqRepository<Conversation> conversationRepository)
		{
            _conversationRepository = conversationRepository;
			_userRepository = userInterestRepository;
		}

		#region IInterestTasks Members

        public bool SendMessage(String from, String to, String text)
        {
            //get the sender logged in
            var currentUserQuery = new AdHoc<User>(x => x.Username == from);
            var sender = _userRepository.FindOne(currentUserQuery);

            //get the recipient
            var recipientQuery = new AdHoc<User>(x => x.Username == to);
            var recipient = _userRepository.FindOne(recipientQuery);

            if (sender == null || recipient == null)
            {
                return false;
            }

            //both users exists
            //see if the recipient has any active conversations with the sender


            //get all the conversations where the recipient is an active participant
            var conversations = recipient.Participants.Where(x => x.IsActive).Select(x => x.Conversation).ToList();

            //get the conversation that the recipient has with the sender
            var conversation = conversations.SingleOrDefault(x => x.Participants.Any(y => y.User == sender));

            if (conversation == null)
            {
                //start a new conversation
                conversation = new Conversation(sender, recipient);
            }

            Message message = sender.Say(text);
            conversation.AddMessage(message);
            _conversationRepository.Save(conversation);

            return true;
        }

        public Message Reply(string from, int conversationId, string text)
        {
            Message retVal = null;
            var senderUserQuery = new AdHoc<User>(x => x.Username == from);
            var sender = _userRepository.FindOne(senderUserQuery);

            if (sender != null)
            {
                var conversation = sender.Participants.SingleOrDefault(x => x.Conversation.Id == conversationId).Conversation;

                if (conversation != null)
                {
                    //AdHoc<Conversation> conversationQuery = new AdHoc<Conversation>(conv => conv.Id == conversationId && conv.Participants.Any(part => part.User.Username == from));
                    //var conversation = _conversationRepository.FindOne(conversationQuery);

                    retVal = sender.Say(text);
                    conversation.AddMessage(retVal);
                }
                _conversationRepository.Save(conversation);
            }
            return retVal;
        }
		
        #endregion
    }
}