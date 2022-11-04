using NotificationReceiverLibrary;
using RabbitMQ.Client;
using System;
using System.Text;


Receiver receiver = new Receiver();
string qname = "";
string message = "No Message";

receiver.OpenConnection();

receiver.DeclareExchange("SubscriberExchange", ExchangeType.Fanout);

receiver.BindQueue("SubscriberExchange", "UR.Robot.Status.NotficationSys");

qname = receiver.queueNames.First();

receiver.Receiving();

receiver.Consume(qname);

Console.WriteLine("Something");
Console.ReadLine();