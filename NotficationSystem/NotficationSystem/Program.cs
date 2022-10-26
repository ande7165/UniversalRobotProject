using NotificationReceiverLibrary;
using RabbitMQ.Client;
using System;
using System.Text;

bool running = true;
Receiver receiver = new Receiver();
string qname = "";
string message = "No Message";
//try
//{
	
	receiver.OpenConnection();

	receiver.DeclareExchange("URStatus", ExchangeType.Fanout);

	receiver.BindQueue("URStatus", "UR.Robot.Status");

	qname = receiver.queueNames.First();
	//while(running)
	receiver.Receiving();
	
	receiver.Consume(qname);

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







