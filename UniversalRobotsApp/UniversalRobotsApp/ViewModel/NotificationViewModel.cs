using UniversalRobotsApp.Model;

namespace UniversalRobotsApp.ViewModel
{
	public class NotificationViewModel
	{
		public Notification SelectedNotification { get; set; }
		public NotificationViewModel(Notification selectedNotification)
		{
			SelectedNotification = selectedNotification;
		}
	}
}
