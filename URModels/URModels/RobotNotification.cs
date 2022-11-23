namespace URModels
{
	public enum Status
	{
		OK,
		Warning,
		Error,
	}
	public class RobotNotification
	{
		public string RobotId { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public Status NotificationStatus { get; set; }
		public DateTime NotificationTimeStamp { get; set; }
	}
}