using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.ReadModels;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface IPostTasks
	{
        IList<UserPostDto> GetPostsByInterests(IList<Interest> interests);
		void SaveComment(SavePostCommentDto postCommentDto, User user);
		void SaveLike(int postId, User user);
	    Post Get(int id);
	    void Save(Post post);
	}
}