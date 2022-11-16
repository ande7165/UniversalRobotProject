namespace UniversalRobotsApp.Pages;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}
	public void LogoutAction(object sender, EventArgs e)
	{
		((App)Application.Current).MainPage = new LoginPage();
	}
}