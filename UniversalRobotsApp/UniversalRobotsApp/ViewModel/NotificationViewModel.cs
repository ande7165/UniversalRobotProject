using UniversalRobotsApp.Model;
using URModels;

namespace UniversalRobotsApp.ViewModel
{
	public class NotificationViewModel
	{
		public RobotNotification SelectedNotification { get; set; }
		public NotificationViewModel(RobotNotification selectedNotification)
		{
			SelectedNotification = selectedNotification;
		}
	}
}
