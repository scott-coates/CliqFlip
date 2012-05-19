using System;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
	public  class InterestFeedQuery : IInterestFeedQuery
	{
		public InterestFeedViewModel GetGetUsersByInterests(string userName, int? page)
		{
			//get user interests

			//get sibling and parent interests

			//get all media with any matching interests order by date desc limit by 100

			//rank them in order then grab that page

			/*
			 * my thinking is take the last 100 because a user probably won't read past that..
			 * if we only grab the first 10 then there is the problem that 11th might be extremely relevent 
			 * but shows up much later on cause it's
			 * slightly older
			 */

			return null;
		}
	}
}