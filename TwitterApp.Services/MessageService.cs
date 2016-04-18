using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Contracts;

namespace TwitterApp.Services {

	public class MessageService : IMessageService {
		private IList<TwitterMessage> _messages;

		private const int MAX_MESSAGE_SIZE = 144;

		public MessageService() {
			_messages = new List<TwitterMessage>();
		}

		public IList<TwitterMessage> GetMessagesFor(Guid userId) {
			return _messages
				.Where(x => x.ToId == userId)
				.ToList();
		}

		public IList<TwitterMessage> GetPostsBy(Guid userId) {
			return _messages
				.Where(x => x.FromId == userId && x.ToId == null)
				.ToList();
		}

		/*
		 * Posting messages is used for creating a message that anybody can see and isn't directed
		 * to a specific person.
		 */
		public void Post(TwitterMessage message) {
			if (message.ToId != null) {
				throw new Exception("Message cannot be posted to a person.\nMaybe try send?");
			}

			if (message.FromId == null) {
				throw new Exception("Message cannot be posted by a null user.");
			}

			if (message.Message.Length > MAX_MESSAGE_SIZE) {
				throw new Exception("Message cannot be larger than " + MAX_MESSAGE_SIZE);
			}

			_messages.Add(message);
		}

		/*
		 * A message should only be sent if the ToId is filled in.  Otherwise the message would go
		 * to nobody.
		 */
		public void Send(TwitterMessage message) {
			if (message.FromId == null) {
				throw new Exception("Message cannot come from a null user.");
			}

			if (message.ToId == null) {
				throw new Exception("Unable to send message to null user.\nMaybe try post?");
			}

			if (message.Message.Length > MAX_MESSAGE_SIZE) {
				throw new Exception("Message cannot be larger than " + MAX_MESSAGE_SIZE);
			}

			_messages.Add(message);
		}

	}

}
