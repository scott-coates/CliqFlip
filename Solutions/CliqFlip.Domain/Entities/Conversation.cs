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
        private Iesi.Collections.Generic.ISet<User> _participants = new HashedSet<User>();
        private Iesi.Collections.Generic.ISet<Participant> _alerts = new HashedSet<Participant>();

        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<User> Participants { get; set; }
        
        public Conversation()
        {
            var user = new User();
        }

        /// <summary>
        /// a conversation between two people
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        public Conversation(User user1, User user2)
        {
            _participants.Add(user1);
            _participants.Add(user2);
        }

        public void AddMessage(Message message)
        {
            var usersToNotify = _participants.ToList();
            usersToNotify.Remove(message.Sender); //do not notify the user that just send the message

            //get a list of all the users who already have an active alert for this conversation
            var usersPreviouslyAlerted = _alerts.Where(alert => alert.HasUnreadMessages).Select(alert => alert.User).ToList();


            //usersPreviouslyAlerted.Except(
            var usersToAlert = _participants.Except(usersPreviouslyAlerted);
            foreach (var user in usersToAlert)
            {
                var alert = new Participant { Conversation = this, User = user, HasUnreadMessages = true };
                _alerts.Add(alert);

            }

            _messages.Add(message);
        }

        public bool Remove(User user)
        {
            return _participants.Remove(user);
        }
    }
}