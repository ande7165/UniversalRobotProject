using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationReceiverLibrary
{
	public class Receiver
	{
		private string hostName;
		private ConnectionFactory factory;
		private List<string> queueNames;
		private IConnection connection;
		private IModel channel;
		public Receiver(string Host = "localhost")
		{
			if (!Host.Equals("localhost"))
				hostName = Host;

		}
		public void OpenConnection()
		{
			var factory = new ConnectionFactory() { HostName = hostName };
			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();
		}

		public void OpenQueue(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
		{
			if (factory == null || connection == null || channel == null)
				return;

			channel.QueueDeclare(queue: queueName,
								durable: durable,
								exclusive: exclusive,
								autoDelete: autoDelete,
								arguments: arguments);
		}
		public void DeclareExchange(string exchange, string routingKey, string exchangeType)
		{
			channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
		}
		public void BindQueue(string exchange, string routingKey)
		{
			string queueName = channel.QueueDeclare().QueueName;
			channel.QueueBind(queue: queueName,
							  exchange: exchange,
							  routingKey: routingKey);

			queueNames.Append(queueName);
		}
		public void Recevier(string queueName, bool autoAck = true)
		{

			Console.WriteLine(" Wating for message");

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine(message);
			};
			channel.BasicConsume(queue: queueName,
								autoAck: autoAck,
								consumer: consumer);

			Console.WriteLine("Press Enter to exit");
			Console.ReadLine();
		}
		public void DisposeConnection()
		{
			if (factory == null || connection == null || channel == null)
				return;

			connection.Dispose();
		}

		public void DisposeChannel()
		{
			if (factory == null || connection == null || channel == null)
				return;

			channel.Dispose();
		}

		public void DisposeQueue(string queueName)
		{
			if (factory == null || connection == null || channel == null)
				return;

			channel.QueueDelete(queue: queueName);
		}

	}
}