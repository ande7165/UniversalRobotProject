using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Plugin.LocalNotification;
using NotificationReceiverLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using NotificationReceiverLibrary.Interface;

namespace UniversalRobotsApp.ForegroundService
{
	[Service]
	public class NotificationService : Service, IForegroundService, INotificationReceiver
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
			string qname = "";

			receiver.OpenConnection();

			receiver.DeclareExchange("URStatus", ExchangeType.Fanout);

			receiver.BindQueue("URStatus", "UR.Robot.Status");

			qname = receiver.queueNames.First();

			receiver.Receiving(this);

			receiver.Consume(qname);

			

		}

		public void MessageReceiveAction(string message)
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
			var request = new NotificationRequest
			{
				NotificationId = 1,
				Title = $"{message}",
				Subtitle = "Test",
				Description = "Test Desciption"
			};

			LocalNotificationCenter.Current.Show(request);
			#endregion
		}
	}
}
