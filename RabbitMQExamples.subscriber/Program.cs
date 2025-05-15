
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQExamples.subscriber;

class Program
{
	static async Task Main(string[] args)
	{
		var factory = new ConnectionFactory();
		factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

		using var connection = factory.CreateConnection();
		var channel = connection.CreateModel();
		//channel.QueueDeclare("hello-queue", true, false, false);

		var consumer = new EventingBasicConsumer(channel);
		channel.BasicConsume("hello-queue", true, consumer);
		consumer.Received += (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = System.Text.Encoding.UTF8.GetString(body);
			Console.WriteLine($"Mesaj alındı: {message}");
			//channel.BasicAck(ea.DeliveryTag, false);
		};


		Console.ReadLine();
	}
}
