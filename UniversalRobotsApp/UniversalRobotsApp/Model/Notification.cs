using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalRobotsApp.Model
{
	public class Notification
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public Status NotificationStatus { get; set; }
		public DateTime NotificationTimeStamp { get; } = DateTime.Now;

	}
}
