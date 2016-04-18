using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Contracts {

	public class TwitterMessage {

		public Guid RecId { get; set; }
		public Guid? FromId { get; set; }
		public Guid? ToId { get; set; }
		public string Message { get; set; }

		public TwitterMessage() {
			RecId = Guid.NewGuid();
		}

	}

}
