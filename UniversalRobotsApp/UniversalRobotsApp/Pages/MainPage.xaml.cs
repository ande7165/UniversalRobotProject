using Android.Widget;
using Plugin.LocalNotification;
using UniversalRobotsApp.ForegroundService;

namespace UniversalRobotsApp.Pages;

public partial class MainPage : ContentPage
{
	public IForegroundService Service { get; set; }
	public MainPage(/*IForegroundService service*/)
	{

		InitializeComponent();

		Service = new NotificationService();//service;

	}

	private void StartService(object sender, EventArgs e)
	{
		Service.Start();
	}

	private void EndService(object sender, EventArgs e)
	{
		Service.Stop();
	}
}