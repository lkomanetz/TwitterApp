using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Contracts {

	public interface IMessageService {

		void Send(TwitterMessage message);
		void Post(TwitterMessage message);
		IList<TwitterMessage> GetMessagesFor(Guid userId);
		IList<TwitterMessage> GetPostsBy(Guid userId);

	}

}
