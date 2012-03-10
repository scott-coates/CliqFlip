using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;
using Iesi.Collections.Generic;

namespace CliqFlip.Domain.Entities
{
    public class Conversation : Entity
    {
        private IList<Message> _messages = new List<Message>();
        private Iesi.Collections.Generic.ISet<Participant> _participants = new HashedSet<Participant>();

        public virtual IEnumerable<Message> Messages
        {
            get { return new List<Message>(_messages).AsReadOnly(); }
        }
        public virtual IEnumerable<Participant> Participants
        {
            get { return new List<Participant>(_participants).AsReadOnly(); }
        }
        
        public Conversation()
        {

        }

        /// <summary>
        /// a conversation between two people
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        public Conversation(User sender, User recipient)
        {
            _participants.Add(new Participant { IsActive =  true, User = sender, Conversation = this, HasUnreadMessages = false });
            _participants.Add(new Participant { IsActive = true, User = recipient, Conversation = this, HasUnreadMessages = true });
        }

        public virtual void AddMessage(Message message)
        {
            var participantsToNotify = _participants.ToList();
            //remove the sender from the participants to notify
            participantsToNotify.RemoveAll(x => x.User == message.Sender);//Except(.Remove(message.Sender); //do not notify the user that just send the message

            ////get a list of all the users who already have an active alert for this conversation
            //var usersPreviouslyAlerted = _participants.Where(alert => alert.HasUnreadMessages).Select(alert => alert.User).ToList();
            message.Conversation = this;
            foreach (var participant in participantsToNotify)
            {
                participant.HasUnreadMessages = true;
            }
            _messages.Add(message);
        }

        public virtual bool Remove(User user)
        {
            return true;
            //return _participants.Remove(user);
        }

        public virtual bool HasNewMessagesFor(User user)
        {
            return _participants.Any(x => x.HasUnreadMessages && x.User == user);
        }
    }
}