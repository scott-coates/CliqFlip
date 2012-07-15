using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
    public class PostTasks : IPostTasks
    {
        private readonly IPostRepository _postRepository;

        public PostTasks(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void SaveComment(SavePostCommentDto postCommentDto, User user)
        {
            var post = _postRepository.Get(postCommentDto.PostId);
            post.AddComment(postCommentDto.CommentText, user);
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }
    }
}