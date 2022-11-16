namespace UniversalRobotsApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	public void LoginAction(object sender, EventArgs e)
	{
		((App)Application.Current).MainPage = new AppShell();
	}
}