using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Contracts;
using TwitterApp.Services;

namespace TwitterApp {

	class Program {
		private static IMessageService _messageService;
		private static IUserService _userService;

		static void Main(string[] args) {
			StartServices();

			TwitterUser firstUser = new TwitterUser() {
				FirstName = "Logan",
				LastName = "Komanetz",
				Username = "lkomanetz",
				Password = "Password"
			};

			TwitterUser secondUser = new TwitterUser() {
				FirstName = "Noridian",
				LastName = "Insurance",
				Username = "nInsurance",
				Password = "Passw0rd"
			};

			_userService.Login(firstUser);
			_userService.Login(secondUser);
			_userService.Follow(secondUser.RecId, firstUser.RecId);

			_messageService.Post(new TwitterMessage() {
				FromId = firstUser.RecId,
				Message = "Recently lost my job :-(.  Looking for great opportunity!"
			});
			_messageService.Post(new TwitterMessage() {
				FromId = secondUser.RecId,
				Message = "Noridian is looking for new applications!"
			});
			_messageService.Send(new TwitterMessage() {
				FromId = firstUser.RecId,
				ToId = secondUser.RecId,
				Message = "Are you looking for developers?"
			});
			_messageService.Send(new TwitterMessage() {
				FromId = secondUser.RecId,
				ToId = firstUser.RecId,
				Message = "We absolutely are!"
			});

			OutputPostsFor(firstUser.RecId);
			OutputPostsFor(secondUser.RecId);
			OutputMessagesFor(firstUser.RecId);
			OutputMessagesFor(secondUser.RecId);
			Console.ReadLine();
		}

		private static void OutputMessagesFor(Guid userId) {
			IList<TwitterMessage> messages = _messageService.GetMessagesFor(userId);
			TwitterUser user = _userService.GetUser(userId);

			Console.WriteLine("================");
			Console.WriteLine("Messages for {0}", user.Username);
			Console.WriteLine("================");
				
			foreach (var message in messages) {
				Guid fromId = (message.FromId.HasValue) ? message.FromId.Value : Guid.Empty;
				Guid toId = (message.ToId.HasValue) ? message.ToId.Value : Guid.Empty;

				TwitterUser from = _userService.GetUser(fromId);
				TwitterUser to = _userService.GetUser(toId);

				String output = String.Format(
					"From: {0}\nTo: {1}\n\n",
					from.Username,
					to.Username
				);
				output += "Message:\n" + message.Message;
				Console.WriteLine(output + '\n');
			}
		}

		private static void OutputPostsFor(Guid userId) {
			IList<TwitterMessage> posts = _messageService.GetPostsBy(userId);
			TwitterUser user = _userService.GetUser(userId);

			Console.WriteLine("====================");
			Console.WriteLine("Posts for {0}", user.Username);
			Console.WriteLine("====================");

			foreach (var post in posts) {
				Guid fromId = (post.FromId.HasValue) ? post.FromId.Value : Guid.Empty;

				TwitterUser from = _userService.GetUser(fromId);

				String output = String.Format(
					"From: {0}\n\n",
					from.Username
				);
				output += "Message:\n" + post.Message;
				Console.WriteLine(output + '\n');
			}
		}

		private static void StartServices() {
			_messageService = new MessageService();
			_userService = new UserService();
		}
	}

}
