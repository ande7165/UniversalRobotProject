using NotificationReceiverLibrary.Interface;
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
		public string message = "";
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

		public string OpenQueue(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
		{
			if (factory == null || connection == null || channel == null)
				throw new NullReferenceException("Factory, channel or connection are null");

			if (string.IsNullOrEmpty(queueName))
			{
				queueName = channel.QueueDeclare().QueueName;
			}
			else
			{
				channel.QueueDeclare(queue: queueName,
					durable: durable,
					exclusive: exclusive,
					autoDelete: autoDelete,
					arguments: arguments);
			}

			return queueName;
		}
		public void DeclareExchange(string exchange, string exchangeType)
		{
			channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
		}
		public void BindQueue(string exchange, string routingKey, string queue)
		{

			string queueName = queue;//channel.QueueDeclare().QueueName;
			channel.QueueBind(queue: queueName,
							  exchange: exchange,
							  routingKey: routingKey);

			queueNames.Add(queueName);
		}
		/// <summary>
		/// Receive message sent by rabbitmq
		/// </summary>
		/// <param name="action">delegate that executes with the message as parameter, here you can do whatever you deem necessary</param>
		/// <returns>bool to confirm whether it received or not</returns>
		public bool Receiving(Action<string> action)
		{

			Console.WriteLine(" Wating for message");

			consumer = new EventingBasicConsumer(channel);

			bool received = false;

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				message = Encoding.UTF8.GetString(body);
				action(message);

				received = true;
			};

			return received;
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