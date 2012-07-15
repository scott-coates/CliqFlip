using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.Post
{
    public class PostDtoWithComments : PostDto
    {
        public int CommentCount { get; set; }
        public IList<CommentDto> Comments { get; set; }

        public PostDtoWithComments(Entities.Post post) : base(post)
        {
            var comments = post.Comments.ToList();
            Comments = comments.Take(2).Select(x => new CommentDto(x)).ToList();
            CommentCount = comments.Count;
        }
    }
}