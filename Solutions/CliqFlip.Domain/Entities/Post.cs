using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
    /*TODO: Post should not fall under the UserInterest Aggregate
     * It makes sense for a post to exist if the user deltes an interest
    */

    public class Post : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<Comment> _comments;
        private readonly Iesi.Collections.Generic.ISet<Like> _likes;

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

        public virtual IEnumerable<Like> Likes
        {
            get { return new List<Like>(_likes).AsReadOnly(); }
        }

        public Post()
        {
            _comments = new HashedSet<Comment>();
            _likes = new HashedSet<Like>();
        }

        public virtual void AddComment(string commentText, User user)
        {
            _comments.Add(
                new Comment
                {
                    CommentText = commentText,
                    CreateDate = DateTime.UtcNow,
                    Post = this,
                    User = user
                });
        }

        public virtual void AddLike(User user)
        {
            _likes.Add(new Like { CreateDate = DateTime.UtcNow, Post = this, User = user });
        }
    }
}