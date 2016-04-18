using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Contracts {

	public class TwitterUser {

		public Guid RecId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public TwitterUser() {
			RecId = Guid.NewGuid();
		}

		public override int GetHashCode() {
			return RecId.GetHashCode();
		}

		public override bool Equals(object obj) {
			bool isEqual = ReferenceEquals(this, obj);
		
			if (isEqual) {
				return true;
			}	

			TwitterUser secondObj = obj as TwitterUser;
			if (secondObj == null) {
				return false;
			}

			return this.FirstName == secondObj.FirstName &&
				this.LastName == secondObj.LastName &&
				this.Username == secondObj.Username;
		}

		public static bool operator ==(TwitterUser objA, TwitterUser objB) {
			if (Object.ReferenceEquals(objA, objB)) {
				return true;
			}

			if ((object) objA == null || (object)objB == null) {
				return false;
			}

			return objA.Equals(objB);
		}

		public static bool operator !=(TwitterUser objA, TwitterUser objB) {
			return (!objA.Equals(objB));
		}
	}

}
