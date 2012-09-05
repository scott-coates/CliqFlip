using System.Collections.Generic;
using System.Linq;

namespace CliqFlip.Domain.Dtos.Post
{
    public class PostDtoWithActivity : PostDto
    {
        public int CommentCount { get; set; }
        public IList<CommentDto> Comments { get; set; }
        public IList<LikeDto> Likes { get; set; }

        public PostDtoWithActivity(ReadModels.Post post): base(post)
        {
            var comments = post.Comments.ToList();
            var likes = post.Likes.ToList();

            Comments = comments.Select(x => new CommentDto(x)).ToList();
            CommentCount = comments.Count;
            
            Likes = likes.Select(x => new LikeDto { UserId = x.User.Id }).ToList();
        }
    }
}