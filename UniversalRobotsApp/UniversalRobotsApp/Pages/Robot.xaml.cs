using UniversalRobotsApp.ViewModel;

namespace UniversalRobotsApp.Pages;

public partial class Robot : ContentPage
{
	public Robot(RobotViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
	public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	{
		if (args.SelectedItem != null)
		{
			var vm = new NotificationViewModel((Model.Notification)args.SelectedItem);

			await Navigation.PushModalAsync(new Pages.NotificationPage { BindingContext = vm});

			NotificationsList.SelectedItem = null;
		}
	}
}