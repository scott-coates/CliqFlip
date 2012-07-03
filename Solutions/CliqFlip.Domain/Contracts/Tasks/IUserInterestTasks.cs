﻿using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface IUserInterestTasks
	{
		IList<InterestFeedItemDto> GetPostsByInterests(IList<Interest> interests);
	}
}