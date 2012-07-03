using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CliqFlip.Domain.ValueObjects;
using Iesi.Collections.Generic;
using SharpArch.Domain.DomainModel;
using nh = Iesi.Collections.Generic;

namespace CliqFlip.Domain.Entities
{
    public class User : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<UserInterest> _interests;
        private readonly Iesi.Collections.Generic.ISet<Conversation> _conversations;

        private UserWebsite _userWebsite;

        public virtual IEnumerable<UserInterest> Interests
        {
            get { return new List<UserInterest>(_interests).AsReadOnly(); }
        }

        public virtual IEnumerable<Conversation> Conversations
        {
            get { return new List<Conversation>(_conversations).AsReadOnly(); }
        }

        /*
			//if we ever have a collection of images (user.images), we'll need to set cascade.noaction
			//then there would be a cycle between user and userinterests to images
			//set this to no action otherwise a cascade error will occur
			//http://stackoverflow.com/questions/851625/foreign-key-constraint-may-cause-cycles-or-multiple-cascade-paths
			//we'll need to manually delete profile images before removing a user
		 */

        public virtual Image ProfileImage { get; set; }
        public virtual Location Location { get; set; }

        public virtual UserWebsite UserWebsite
        {
            //http://stackoverflow.com/a/685026/173957
            get { return _userWebsite ?? new UserWebsite(null, null); }
            set { _userWebsite = value; }
        }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual string Username { get; set; }

        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual string Bio { get; set; }
        public virtual string Headline { get; set; }
        public virtual string TwitterUsername { get; set; }
        public virtual string YouTubeUsername { get; set; }
        public virtual string FacebookUsername { get; set; } //TODO:rename to facebook access code
        public virtual string Role { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime LastActivity { get; set; }

        public User()
        {
            _interests = new HashedSet<UserInterest>();
            _conversations = new HashedSet<Conversation>();
        }

        public User(string username, string email, string password, string salt)
            : this()
        {
            Username = username;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public virtual UserInterest AddInterest(Interest interest, int? socialityPoints)
        {
            var existingUserInterest = _interests.FirstOrDefault(x => x.Interest == interest);

            if (existingUserInterest == null)
            {
                existingUserInterest = new UserInterest
                {
                    User = this,
                    Interest = interest,
                    SocialityPoints = socialityPoints
                };

                _interests.Add(existingUserInterest);
            }

            UpdateLastActivity();

            return existingUserInterest;
        }

        public virtual UserInterest UpdateInterest(int userInterestId, UserInterestOption userInterestOption)
        {
            UserInterest userInterest = _interests.First(x => x.Id == userInterestId);
            userInterest.Options = userInterestOption;

            UpdateLastActivity();

            return userInterest;
        }

        public virtual void UpdateHeadline(string headline)
        {
            Headline = !string.IsNullOrWhiteSpace(headline) ? headline.Trim() : null;
            UpdateLastActivity();
        }

        public virtual void UpdateBio(string bio)
        {
            Bio = !string.IsNullOrWhiteSpace(bio) ? bio.Trim() : null;
            UpdateLastActivity();
        }

        public virtual void UpdateTwitterUsername(string twitterUsername)
        {
            TwitterUsername = !string.IsNullOrWhiteSpace(twitterUsername) ? twitterUsername.Trim() : null;
            UpdateLastActivity();
        }

        public virtual void UpdateYouTubeUsername(string youTubeUsername)
        {
            //white space before/after in the username causes problems when making a request to the youtube api
            YouTubeUsername = !string.IsNullOrWhiteSpace(youTubeUsername) ? youTubeUsername.Trim() : null;
            UpdateLastActivity();
        }

        public virtual void UpdateProfileImage(ImageData data)
        {
            if (ProfileImage == null)
            {
                ProfileImage = new Image();
            }

            ProfileImage.ImageData = data;
            UpdateLastActivity();
        }

        public virtual void UpdateWebsite(string siteUrl, string feedUrl)
        {
            _userWebsite = new UserWebsite(siteUrl, feedUrl);
            UpdateLastActivity();
        }

        public virtual void UpdateLastActivity()
        {
            LastActivity = DateTime.UtcNow;
            //TODO: use interceptor
        }

        public virtual void UpdateCreateDate()
        {
            CreateDate = DateTime.UtcNow;
            UpdateLastActivity();
        }

        public virtual void UpdateFacebookUsername(string fbid)
        {
            FacebookUsername = !string.IsNullOrWhiteSpace(fbid) ? fbid.Trim() : null;
            UpdateLastActivity();
        }

        public virtual void MakeInterestPostDefault(int postId)
        {
            Post post = GetPost(postId);
            post.UserInterest.MakePostDefault(post);
            UpdateLastActivity();
        }

        public virtual Post GetPost(int postId)
        {
            return _interests.SelectMany(x => x.Posts).First(x => x.Id == postId);
        }

        public virtual void RemoveInterestPost(Post post)
        {
            post.UserInterest.RemoveInterestPost(post);
            UpdateLastActivity();
        }

        public virtual UserInterest GetInterest(int interestId)
        {
            return _interests.First(x => x.Id == interestId);
        }

        public virtual void RemoveInterest(UserInterest interest)
        {
            _interests.Remove(interest);
            UpdateLastActivity();
        }

        public virtual void ReadConversation(int id)
        {
            var conversation = _conversations.SingleOrDefault(x => x.Id == id);

            //only mark the conversation as read if the user reading it
            //is the one who has a new message waiting for them
            if (conversation != null && conversation.HasNewMessagesFor(this))
            {
                conversation.HasUnreadMessages = false;
            }
            UpdateLastActivity();
        }

        public virtual Message WriteMessage(string text)
        {
            UpdateLastActivity();
            return new Message(this, text);
        }

        public virtual int GetNumberOfUnreadConversations()
        {
            return _conversations.Count(x => x.HasNewMessagesFor(this));
        }

        protected internal virtual void Subscribe(Conversation conversation)
        {
            _conversations.Add(conversation);
        }

        public virtual void UpdateLocation(LocationData location, MajorLocation majorLocation)
        {
            if (Location == null)
            {
                Location = new Location();
            }

            Location.Data = location;
            Location.MajorLocation = majorLocation;

            UpdateLastActivity();
        }

        public virtual void UpdateEmail(string email)
        {
            //TODO: put validation on domain entity too
            Email = email;
            UpdateLastActivity();
        }

        public virtual void UpdatePassword(string password, string salt)
        {
            Password = password;
            Salt = salt;
            UpdateLastActivity();
        }
    }
}