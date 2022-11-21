using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalRobotsApp.Model;

namespace UniversalRobotsApp.ViewModel
{
	public class RobotViewModel
	{
		public Robot SelectedRobot { get; set; }

		public RobotViewModel(Robot selectedRobot)
		{
			SelectedRobot = selectedRobot;
		}
	}
}
