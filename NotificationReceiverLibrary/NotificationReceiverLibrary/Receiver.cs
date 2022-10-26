using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationReceiverLibrary
{
	public class Receiver
	{
		private string hostName;
		private ConnectionFactory factory;
		public List<string> queueNames = new List<string>();
		public EventingBasicConsumer consumer;
		private IConnection connection;
		private IModel channel;
		public Receiver(string Host = "localhost")
		{
			hostName = Host;
		}
		public void OpenConnection()
		{
			factory = new ConnectionFactory() { HostName = hostName };
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
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
		public void DeclareExchange(string exchange, string exchangeType)
		{
			channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
		}
		public void BindQueue(string exchange, string routingKey)
		{
			string queueName = channel.QueueDeclare().QueueName;
			channel.QueueBind(queue: queueName,
							  exchange: exchange,
							  routingKey: routingKey);

			queueNames.Add(queueName);
		}
		public void Receiving()
		{

			Console.WriteLine(" Wating for message");

			consumer = new EventingBasicConsumer(channel);

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				string message = Encoding.UTF8.GetString(body);
				Console.WriteLine(message);
			};
		}

		public void Consume(string queueName, bool autoAck = true)
		{
			channel.BasicConsume(queue: queueName,
								autoAck: autoAck,
								consumer: consumer);
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
		public void DisposeExchange(string exchange)
		{
			if (factory == null || connection == null || channel == null)
				return;

			channel.ExchangeDelete(exchange: exchange);
		}
	}
}