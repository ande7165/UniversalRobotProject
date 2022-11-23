using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Plugin.LocalNotification;
using NotificationReceiverLibrary;
using RabbitMQ.Client;
using URModels;
using System.Text.Json;

namespace UniversalRobotsApp.ForegroundService
{
	[Service]
	public class NotificationService : Service, IForegroundService
	{
		public bool ServiceRunning { get; set; }
		public override IBinder OnBind(Intent intent)
		{
			throw new NotImplementedException();
		}

		[return: GeneratedEnum]//we catch the actions intents to know the state of the foreground service
		public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
		{
			if (intent.Action == "START_SERVICE")
			{
				if (!ServiceRunning)
				{
					LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
					RabbitMQReceiver();

					ServiceRunning = true;
				}
					
			}
			else if (intent.Action == "STOP_SERVICE")
			{
				StopForeground(true);//Stop the service
				StopSelfResult(startId);
			}
			return StartCommandResult.Sticky;
		}

		private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
		{
			if (e.IsDismissed)
			{

			}
			else if (e.IsTapped)
			{
				Toast.MakeText(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, "Hello", ToastLength.Short).Show();
			}
		}

		//Start and Stop Intents, set the actions for the MainActivity to get the state of the foreground service
		//Setting one action to start and one action to stop the foreground service
		public void Start()
		{
			//Toast.MakeText(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, "Start", ToastLength.Short).Show();
			Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(NotificationService));
			startService.SetAction("START_SERVICE");
			MainActivity.ActivityCurrent.StartService(startService);
		}

		public void Stop()
		{
			//Toast.MakeText(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, "Stop", ToastLength.Short).Show();
			Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
			stopIntent.SetAction("STOP_SERVICE");
			MainActivity.ActivityCurrent.StartService(stopIntent);
			ServiceRunning = false;
		}

		private void RabbitMQReceiver()
		{
			Receiver receiver = new Receiver("192.168.1.92");

			receiver.OpenConnection();

			receiver.DeclareExchange("NotificationSystem", ExchangeType.Fanout);

			string qname = receiver.OpenQueue("");

			receiver.BindQueue("NotificationSystem", "UR.Robot.Status.NotificationSys", qname);

			receiver.Receiving((string m) => MessageReceiveAction(DeserializeMessage(m)));

			receiver.Consume(qname);
			

		}

		private RobotNotification DeserializeMessage(string message)
		{
			RobotNotification robotNotification = JsonSerializer.Deserialize<RobotNotification>(message);
			
			return robotNotification;
		}

		public void MessageReceiveAction(RobotNotification notification)
		{
			#region Toast for Testing method
			//Toast.MakeText(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, "Stop", ToastLength.Short).Show();
			#endregion
			#region Android native notification 
			//NotificationChannel channel = new NotificationChannel("ServiceChannel", "NotificationService", NotificationImportance.Max);
			//NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
			//manager.CreateNotificationChannel(channel);
			//Notification notification = new Notification.Builder(this, "ServiceChannel")
			//		.SetContentTitle("Service Working")
			//		.SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
			//		.SetOngoing(true)
			//		.Build();

			//StartForeground(100, notification);
			#endregion
			#region Local Push Notification Library
			if(notification != null)
			{
				var request = new NotificationRequest
				{
					NotificationId = 1,
					Title = $"Robot {notification.RobotId}:{notification.Title}",
					Description = $"{notification.Message}"
				};

				LocalNotificationCenter.Current.Show(request);
			}
			#endregion
		}
	}
}
