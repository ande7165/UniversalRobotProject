using NotificationReceiverLibrary;
using RabbitMQ.Client;
using SenderIntegration;
using System;
using System.Text;

bool running = true;
Receiver receiver = new Receiver();
Sender sender = new Sender();

string qname = "MyURMock";

receiver.OpenConnection();
sender.OpenConnection();

receiver.OpenQueue(qname);

sender.DeclareExchange("NotificationSystem", ExchangeType.Fanout);

receiver.DeclareExchange("URStatus", ExchangeType.Topic);

receiver.BindQueue("URStatus", "UR.Robot.Status", qname);

bool received = receiver.Receiving((string m) => { Console.WriteLine(m); sender.SendMessage(m, "UR.Robot.Status.NotificationSys", "NotificationSystem"); });

receiver.Consume(qname, false);



//sender.OpenConnection();



//while (running)
//{
//	//Console.WriteLine("Press to Exit");
//	//var key = Console.ReadKey().Key;
//	//if(key != ConsoleKey.Escape)
//	//{
//		if (received)
//		{
//			
//			received = false;
//		}
//	//}
//	//else
//	//{
//	//	running = false;
//	//	sender.DisposeQueue("randomqueue");
//	//	sender.DisposeChannel();
//	//	sender.DisposeConnection();
//	//}

//}

Console.WriteLine("Exited");
Console.ReadLine();








