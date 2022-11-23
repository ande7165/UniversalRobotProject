using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URModels;

namespace UniversalRobotsApp.Model
{
	public class Robot
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; } = "Resources/Images/dotnet_bot.svg";
		public Status CurrentStatus { get; set; }

		public ObservableCollection<RobotNotification> Notifications { get; } = new ObservableCollection<RobotNotification>();

		public void AddNotification(RobotNotification notification)
		{
			Notifications.Add(notification);

			CurrentStatus = notification.NotificationStatus;

		}

	}
}
