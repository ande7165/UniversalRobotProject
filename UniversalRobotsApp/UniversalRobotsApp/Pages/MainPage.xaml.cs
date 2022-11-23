using Android.Widget;
using Plugin.LocalNotification;
using UniversalRobotsApp.ForegroundService;

namespace UniversalRobotsApp.Pages;

public partial class MainPage : ContentPage
{
	public IForegroundService Service { get; set; }
	public MainPage()
	{

		InitializeComponent();

#if ANDROID
		Service = new NotificationService();

		Service.Start();
#endif
	}

	private void StartService(object sender, EventArgs e)
	{
		if(Service != null)
			Service.Start();
	}

	private void EndService(object sender, EventArgs e)
	{
		if( Service != null )
			Service.Stop();
	}
}