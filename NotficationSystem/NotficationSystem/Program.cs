using NotificationReceiverLibrary;
using RabbitMQ.Client;
using SenderIntegration;
using System;
using System.Text;

bool running = true;
Receiver receiver = new Receiver();
string qname = "";
string message = "No Message";
//try
//{
	
	//receiver.OpenConnection();

	//receiver.DeclareExchange("URStatus", ExchangeType.Fanout);

	//receiver.BindQueue("URStatus", "UR.Robot.Status");

	//qname = receiver.queueNames.First();

	////while(running)

	//receiver.Receiving();

receiver.message = "Test";
Sender sender = new Sender();

sender.OpenConnection();
sender.OpenQueue("RandomQueue");

while (running)
{
	Console.WriteLine("Press to run");
	var key = Console.ReadKey().Key;
	if(key == ConsoleKey.Enter)
	{
		if (!string.IsNullOrWhiteSpace(receiver.message))
		{
			//string? key = console.readkey().tostring();
			//if (key == "esc")
			//{
			//	running = false;
			//	sender.disposequeue("randomqueue");
			//	sender.disposechannel();
			//	sender.disposeconnection();
			//}
			//else
			sender.SendMessage(receiver.message, "UR.Robot.Status.NotficationSys", "SubscriberExchange");
		}
	}

}





//receiver.Consume(qname);

Console.WriteLine("Something");
Console.ReadLine();


//}
//finally
//{
//	receiver.DisposeQueue(qname);
//	receiver.DisposeChannel();
//	receiver.DisposeExchange();
//	receiver.DisposeConnection();
//}




//while (!running)
//{

//}







