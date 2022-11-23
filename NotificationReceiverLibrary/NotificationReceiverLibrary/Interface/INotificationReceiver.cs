using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationReceiverLibrary.Interface
{
	public interface INotificationReceiver
	{
		void MessageReceiveAction(string message);
	}
}
