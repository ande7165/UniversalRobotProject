using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalRobotsApp.ViewModel
{

	public class RobotsViewModel
	{
		public ObservableCollection<Model.Robot> RobotsList { get; set; } = new ObservableCollection<Model.Robot> {
		new Model.Robot() { Id="id1", Name ="Robot1", Description ="This is Robot1"},
		new Model.Robot() { Id = "id2", Name = "Robot2", Description = "This is Robot2"},
		new Model.Robot() { Id = "id3", Name = "Robot3", Description = "This is Robot3"} };


	}

}
