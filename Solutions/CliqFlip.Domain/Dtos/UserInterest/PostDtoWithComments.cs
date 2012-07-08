using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Dtos.UserInterest
{
    public class PostDtoWithComments : PostDto
    {
        public int CommentCount { get; set; }
        public IList<CommentDto> Comments { get; set; }

        public PostDtoWithComments(Post post) : base(post)
        {
            Comments = post.Comments.Select(x => new CommentDto(x)).ToList();
            CommentCount = Comments.Count;
        }
    }
}