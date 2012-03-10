using System;
using CliqFlip.Web.Mvc.ViewModels;
using System.Collections.Generic;
namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
    public interface IConversationQuery
    {
        IEnumerable<MessageViewModel> GetUserProfileIndex(int conversationId, string username);
    }
}
