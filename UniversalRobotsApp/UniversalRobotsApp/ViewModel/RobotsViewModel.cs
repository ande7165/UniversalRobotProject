using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalRobotsApp.Model;
using URModels;

namespace UniversalRobotsApp.ViewModel
{

	public class RobotsViewModel
	{
		public ObservableCollection<Model.Robot> RobotsList { get; set; } = new ObservableCollection<Model.Robot> ();

		public RobotsViewModel()
		{
			DataDumping();
		}

		private void DataDumping()
		{
			if (RobotsList != null && RobotsList.Count > 0)
				RobotsList.Clear();

			var ran = new Random();

			for (int x = 0; x < 10; x++)
			{
				var robot = new Model.Robot() { Id = "id" + x, Name = "Robot" + x, Description = "This is Robot" + x };
				for (int y = 0; y < 3; y++)
				{
					int randomint = ran.Next(2);
					var notification = new RobotNotification
					{
						Message = $"Message Test {y}",
						Title = $"Robot is in \" {(Status)randomint} \" condition",
						NotificationStatus = (Status)randomint
					};
					robot.AddNotification(notification);
				}
				RobotsList.Add(robot);
			}
		}
	}

}
