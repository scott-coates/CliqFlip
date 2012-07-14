using System.Collections.Generic;
using System.Linq;

namespace CliqFlip.Domain.Dtos.Post
{
    public class PostDtoWithComments : PostDto
    {
        public int CommentCount { get; set; }
        public IList<CommentDto> Comments { get; set; }

        public PostDtoWithComments(Entities.Post post) : base(post)
        {
            Comments = post.Comments.Select(x => new CommentDto(x)).ToList();
            CommentCount = Comments.Count;
        }
    }
}