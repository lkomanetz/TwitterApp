using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Contracts {

	public class TwitterFollow {

		public Guid UserFollowing { get; set; }
		public Guid UserBeingFollowed { get; set; }

	}

}
