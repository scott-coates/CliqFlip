using System;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Comment : Entity
    {
        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public virtual string CommentText { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual int PostCommentOrder
        {
            get
            {
                return Post.Comments.ToList().IndexOf(this) + 1; //why not
            }
        }
    }
}