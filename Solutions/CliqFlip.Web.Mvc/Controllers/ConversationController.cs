using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using System.Security.Principal;
using CliqFlip.Web.Mvc.ViewModels.Conversation;
using CliqFlip.Domain.Contracts.Tasks;
using SharpArch.NHibernate.Web.Mvc;
using CliqFlip.Web.Mvc.ViewModels;
using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class ConversationController : Controller
    {
        private IConversationTasks _conversationTasks;
        private IConversationQuery _conversationQuery;
        private IUserTasks _userTasks;
        private IPrincipal _principal;
        public ConversationController(IConversationTasks conversationTasks, IConversationQuery conversationQuery, IPrincipal principal, IUserTasks userTasks)
        {
            _conversationTasks = conversationTasks;
            _conversationQuery = conversationQuery;
            _principal = principal;
            _userTasks = userTasks;
        }

        //
        // GET: /Conversation/

        [Transaction]
        public ActionResult GetMessages(int id)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            user.ReadConversation(id);
            var model = _conversationQuery.GetMessages(id, _principal.Identity.Name);
            return PartialView(model);
        }

        public ActionResult Reply(int id)
        {
            var model = new ConversationReplyViewModel { Id = id };
            return PartialView(model);
        }

        [HttpPost]
        [Transaction]
        public ActionResult Reply(ConversationReplyViewModel model)
        {
            MessageViewModel retVal = null;
            if (ModelState.IsValid)
            {
                var message = _conversationTasks.Reply(_principal.Identity.Name, model.Id, model.Text);
                retVal = new MessageViewModel(message);
            }
            return PartialView("MessageViewModel", retVal);
        }
    }
}