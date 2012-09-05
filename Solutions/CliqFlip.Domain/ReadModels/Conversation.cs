using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.ReadModels
{
    public class Conversation : Entity
    {
        //the messages are ordered in desc order by SendDate 
        //the newest message will always be the first
        private readonly Iesi.Collections.Generic.ISet<Message> _messages = new HashedSet<Message>();
        private readonly Iesi.Collections.Generic.ISet<User> _users = new HashedSet<User>();
        
        public virtual IEnumerable<Message> Messages
        {
            get { return new List<Message>(_messages).AsReadOnly(); }
        }
        public virtual IEnumerable<User> Users {
            get { return new List<User>(_users).AsReadOnly(); } 
        }

        public virtual bool HasUnreadMessages { get; set; }

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
            AddUser(sender);
            AddUser(recipient);
        }

        public virtual void AddMessage(Message message)
        {
            HasUnreadMessages = true;
            message.Conversation = this;
            _messages.Add(message);
        }

        public virtual bool HasNewMessagesFor(User user)
        {
            //quick fail
            if (!HasUnreadMessages)
            {
                return false;
            }

            //get the last user that send a message
            var lastSender = _messages.First().Sender;

            return lastSender.Id != user.Id;
        }

        private void AddUser(User user)
        {
            user.Subscribe(this);
            _users.Add(user);
        }
    }
}