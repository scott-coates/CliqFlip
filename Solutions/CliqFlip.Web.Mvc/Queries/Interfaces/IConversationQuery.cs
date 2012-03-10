using System;
using CliqFlip.Web.Mvc.ViewModels;
using System.Collections.Generic;
namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
    public interface IConversationQuery
    {
        IEnumerable<MessageViewModel> GetMessages(int conversationId, string username);
    }
}
