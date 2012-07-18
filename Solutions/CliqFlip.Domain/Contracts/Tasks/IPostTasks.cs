﻿using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface IPostTasks
	{
        IList<InterestFeedItemDto> GetPostsByInterests(IList<Interest> interests);
		void SaveComment(SavePostCommentDto postCommentDto, User user);
		void SaveLike(int postId, User user);
	    Post Get(int id);
	}
}