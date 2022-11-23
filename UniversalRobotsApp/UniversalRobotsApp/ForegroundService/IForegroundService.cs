using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalRobotsApp.ForegroundService
{
	public interface IForegroundService
	{
		public bool ServiceRunning { get; set; }
		void Start();
		void Stop();
	}
}
