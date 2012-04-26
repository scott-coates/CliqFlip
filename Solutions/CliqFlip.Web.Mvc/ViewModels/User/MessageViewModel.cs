using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        public string Sender { get; set; }
        public string SenderImageUrl { get; set; }

        public MessageViewModel(Message message)
        {
            Text = message.Text;
            SendDate = message.SendDate;
            Sender = message.Sender.Username;
            SenderImageUrl = message.Sender.ProfileImage != null ? message.Sender.ProfileImage.ImageData.ThumbFileName : Constants.DEFAULT_PROFILE_IMAGE;
        }
    }
}
