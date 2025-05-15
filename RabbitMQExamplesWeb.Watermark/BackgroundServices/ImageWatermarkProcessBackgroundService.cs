using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExamplesWeb.Watermark.Services;

namespace RabbitMQExamplesWeb.Watermark.BackgroundServices;

public class ImageWatermarkProcessBackgroundService : BackgroundService
{
	private readonly RabbitMQClientService _rabbitMQClientService;
	private readonly ILogger<ImageWatermarkProcessBackgroundService> _logger;
	private IModel _channel;
	public ImageWatermarkProcessBackgroundService
		(
		  RabbitMQClientService rabbitMQClientService,
		  ILogger<ImageWatermarkProcessBackgroundService> logger
		)
	{
		_rabbitMQClientService = rabbitMQClientService;
		_logger = logger;
	}
	public override Task StartAsync(CancellationToken cancellationToken)
	{

		_channel = _rabbitMQClientService.Connect();
		_channel.BasicQos(0, 1, false);

		return base.StartAsync(cancellationToken);
	}
	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var consumer  = new AsyncEventingBasicConsumer(_channel);
		_channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);

		consumer.Received += Consumer_Received;

		return Task.CompletedTask;
	}

	private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
	{
		throw new NotImplementedException();
	}

	public override Task StopAsync(CancellationToken cancellationToken)
	{
		return base.StopAsync(cancellationToken);
	}
}
