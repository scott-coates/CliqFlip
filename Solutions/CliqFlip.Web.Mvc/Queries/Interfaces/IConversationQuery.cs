using System;
using System.Collections.Generic;
using CliqFlip.Web.Mvc.ViewModels.User;
namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
    public interface IConversationQuery
    {
        IEnumerable<MessageViewModel> GetMessages(int conversationId, string username);
    }
}
