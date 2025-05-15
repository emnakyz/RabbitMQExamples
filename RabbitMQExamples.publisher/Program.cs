
using RabbitMQ.Client;

namespace RabbitMQExamples.publisher;

class Program
{
	static async Task Main(string[] args)
	{
		var factory = new ConnectionFactory();
		factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

		using var connection =  factory.CreateConnection();
	    var channel =  connection.CreateModel(); 
	    channel.QueueDeclare("hello-queue", true, false, false);

		string message = "Hello World!";
		var body = System.Text.Encoding.UTF8.GetBytes(message);
		channel.BasicPublish(string.Empty, "hello-queue", null, body);

		Console.WriteLine("Mesaj gönderilmiştir.");
		Console.ReadLine();
	}
}
