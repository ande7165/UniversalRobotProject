using UniversalRobotsApp.ViewModel;

namespace UniversalRobotsApp.Pages;

public partial class Robot : ContentPage
{
	public Robot(RobotViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}