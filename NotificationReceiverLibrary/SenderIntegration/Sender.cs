using RabbitMQ;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace SenderIntegration
{
	public class Sender
	{
		private string hostName;
		private ConnectionFactory factory;
		private IConnection connection;
		private IModel channel;
		public Sender(string Host = "localhost")
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
		/// <summary>
		/// Send message via rabbbitmq
		/// </summary>
		/// <param name="message">String message, can be normal string or serialized json</param>
		/// <param name="routingKey"></param>
		/// <param name="exchange"></param>
		/// <param name="basicProperties"></param>
		/// <returns>bool to confirm if it got sent or not</returns>
		public bool SendMessage(string message, string routingKey, string exchange, IBasicProperties basicProperties = null)
		{
			if (factory == null || connection == null || channel == null)
				return false;
			if (message == null)
				return false;
			var messageBody = Encoding.UTF8.GetBytes(message);

			channel.BasicPublish(exchange: exchange,
				routingKey: routingKey,
				basicProperties: basicProperties,
				body: messageBody);

			//Console.WriteLine("Message[{0}] Sent", message);
			return true;
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