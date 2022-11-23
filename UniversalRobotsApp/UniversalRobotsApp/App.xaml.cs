using UniversalRobotsApp.ForegroundService;
using UniversalRobotsApp.Pages;

namespace UniversalRobotsApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage();

	}

}
