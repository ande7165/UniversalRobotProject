using Android.App;
using Android.Content.PM;
using Android.OS;
using UniversalRobotsApp.ForegroundService;

namespace UniversalRobotsApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
	public static MainActivity ActivityCurrent { get; set; }
	public IForegroundService Service { get; set; }
	public MainActivity()
	{
		ActivityCurrent = this;
		Service = null;
	}
	public MainActivity(IForegroundService service)
	{
		ActivityCurrent = this;
		Service = service;
	}

	//protected override void OnStop()
	//{
	//	if(Service != null)
	//		Service.Start();
	//}
}
