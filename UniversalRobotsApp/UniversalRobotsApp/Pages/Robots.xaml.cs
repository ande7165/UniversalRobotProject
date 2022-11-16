using System.Collections.ObjectModel;
using UniversalRobotsApp.Model;
using UniversalRobotsApp.ViewModel;

namespace UniversalRobotsApp.Pages;

public partial class Robots : ContentPage
{
	public Robots()
	{
		InitializeComponent();
	}
	public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	{
		if(args.SelectedItem != null)
		{
			var vm = new RobotViewModel((Model.Robot)args.SelectedItem);

			await Navigation.PushModalAsync(new Pages.Robot(vm));

			RobotsListView.SelectedItem = null;
		}
	}
}