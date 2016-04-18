using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Contracts {

	public interface IUserService {

		void Login(TwitterUser user);
		TwitterUser GetUser(Guid recId);
		void Delete(Guid recId);
		void Update(TwitterUser user);
		void Follow(Guid userToFollowId, Guid userFollowingId);

	}

}
