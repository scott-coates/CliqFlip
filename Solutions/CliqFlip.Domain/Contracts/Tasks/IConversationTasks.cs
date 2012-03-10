using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
    public interface IConversationTasks
    {
        bool SendMessage(String from, String to, String text);

        Message Reply(string from, int conversationId, string text);
    }
}
