using System.Collections.Generic;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Domain.Entities
{
	public class UserInterest : Entity
	{
		private readonly Iesi.Collections.Generic.ISet<Post> _posts;
		private UserInterestOption _options;

		public virtual User User { get; set; }
		public virtual Interest Interest { get; set; }
		public virtual int? SocialityPoints { get; set; }

		public virtual UserInterestOption Options
		{
			//http://stackoverflow.com/a/685026/173957
			get { return _options ?? new UserInterestOption(null, null, null); }
			set { _options = value; }
		}

		public virtual IEnumerable<Post> Posts
		{
            get { return new List<Post>(_posts).AsReadOnly(); }
		}

		public UserInterest()
		{
            _posts = new HashedSet<Post>();
		}

		//TODO: consider law of demeter violation - should we be working with user class instead of directly with userInterest??
		//http://msdn.microsoft.com/en-us/magazine/cc947917.aspx#id0070040 - i think we can skip the law of demeter since we're working
		//directly with user intersts
		public virtual void AddPost(Post post)
		{
			post.UserInterest = this;

			_posts.Add(post);
		}

        public virtual void MakePostDefault(Post post)
		{
			var temp = new List<Post>(_posts);
			_posts.Clear();
			_posts.Add(post);
			temp.Remove(post);
			_posts.AddAll(temp);
		}

		public virtual void RemoveInterestPost(Post post)
		{
			_posts.Remove(post);
		}
	}
}