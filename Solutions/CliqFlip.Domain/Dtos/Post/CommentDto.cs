using CliqFlip.Domain.ReadModels;

namespace CliqFlip.Domain.Dtos.Post
{
    public class CommentDto
    {
        public string Username { get; set; }
        public string CommentText { get; set; }

        public CommentDto(Comment comment)
        {
            Username = comment.User.Username;
            CommentText = comment.CommentText;
        }
    }
}