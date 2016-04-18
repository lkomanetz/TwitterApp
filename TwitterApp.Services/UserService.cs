using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Contracts;

namespace TwitterApp.Services {

	public class UserService : IUserService {
		private IList<TwitterUser> _loggedInUsers;
		private IList<TwitterFollow> _followList;

		public UserService() {
			_loggedInUsers = new List<TwitterUser>();
			_followList = new List<TwitterFollow>();
		}

		public void Login(TwitterUser user) {
			bool exists = _loggedInUsers.Any(x => x == user);
			if (exists) {
				throw new Exception(String.Format("User {0} already exists", user.Username));
			}

			_loggedInUsers.Add(user);
		}

		public void Delete(Guid recId) {
			TwitterUser userToRemove = _loggedInUsers.Where(x => x.RecId == recId).SingleOrDefault();

			if (userToRemove == null) {
				return;
			}

			_loggedInUsers.Remove(userToRemove);
		}

		public void Follow(Guid userToFollowId, Guid userFollowingId) {
			if (userFollowingId == Guid.Empty) {
				throw new Exception("User asking to follow can't be empty Guid");
			}

			if (userToFollowId == Guid.Empty) {
				throw new Exception("User being followed can't be an empty Guid");
			}

			TwitterFollow follow = new TwitterFollow() {
				UserFollowing = userFollowingId,
				UserBeingFollowed = userToFollowId
			};

			bool exists = _followList
				.Any(x => {
					return x.UserFollowing == userFollowingId && x.UserBeingFollowed == userToFollowId;
				});

			if (!exists) {
				_followList.Add(follow);
			}

		}

		public TwitterUser GetUser(Guid recId) {
			return _loggedInUsers
				.Where(x => x.RecId == recId)
				.SingleOrDefault();
		}

		public void Update(TwitterUser user) {
			TwitterUser userToUpdate = _loggedInUsers.Where(x => x.RecId == user.RecId).SingleOrDefault();

			if (userToUpdate == null) {
				return;
			}

			int index = _loggedInUsers.IndexOf(userToUpdate);
			_loggedInUsers[index] = user;
		}

	}

}
