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

namespace CliqFlip.Web.Mvc.Controllers
{
    public class ConversationController : Controller
    {
        private IConversationTasks _conversationTasks;
        private IConversationQuery _conversationQuery;
        private IPrincipal _principal;
        public ConversationController(IConversationTasks conversationTasks, IConversationQuery conversationQuery, IPrincipal principal)
        {
            _conversationTasks = conversationTasks;
            _conversationQuery = conversationQuery;
            _principal = principal;
        }
        //
        // GET: /Conversation/

        public ActionResult GetMessages(int id)
        {
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
                retVal = new MessageViewModel
                {
                    SendDate = message.SendDate,
                    Sender = message.Sender.Username,
                    SenderImageUrl = message.Sender.ProfileImage != null ? message.Sender.ProfileImage.Data.ThumbFileName : "/Content/img/empty-avatar.jpg",
                    Text = message.Text
                };
            }
            return PartialView("MessageViewModel", retVal);
        }
    }
}
