using SenderIntegration;
using System.Text.Json;
using System.Text.Json.Serialization;
using URModels;

bool running = true;
Sender sender = new Sender();

sender.OpenConnection();
sender.OpenQueue("MyURMock");

RobotNotification notif = new RobotNotification
{
	RobotId = "this is id",
	Title = "Test Notification",
	Message = "This is a test notification",
	NotificationStatus = Status.OK,
	NotificationTimeStamp = DateTime.Now
};

string message = JsonSerializer.Serialize(notif);

while (running)
{
	Console.WriteLine("Press Enter to send or escape to exit");
	string? key = Console.ReadKey().ToString();
	if (key == "ESC")
	{
		running = false;
		sender.DisposeQueue("MyURMock");
		sender.DisposeChannel();
		sender.DisposeConnection();
	}
	else
		sender.SendMessage(message, "UR.Robot.Status", "URStatus");
}




