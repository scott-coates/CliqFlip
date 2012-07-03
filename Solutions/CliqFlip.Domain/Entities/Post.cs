using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    public class Post : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<Comment> _comments;

        public virtual UserInterest UserInterest { get; set; }

        public virtual Medium Medium { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual int InterestPostOrder
        {
            get { return UserInterest.Posts.ToList().IndexOf(this) + 1; }
        }

        public virtual IEnumerable<Comment> Comments
        {
            get { return new List<Comment>(_comments).AsReadOnly(); }
        }

        public Post()
        {
            _comments = new HashedSet<Comment>();
        }
    }
}