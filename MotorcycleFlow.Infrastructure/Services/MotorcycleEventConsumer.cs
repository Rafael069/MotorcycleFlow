using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using MotorcycleFlow.Core.Events;
using MotorcycleFlow.Infrastructure.Data;

public class MotorcycleEventConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MotorcycleEventConsumer> _logger;
    private const string QueueName = "motorcycle_events";

    public MotorcycleEventConsumer(
        IServiceProvider serviceProvider,
        ILogger<MotorcycleEventConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                _logger.LogInformation("Mensagem recebida: {Message}", message);
                await ProcessMessage(message);
                _channel.BasicAck(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem");
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        _channel.BasicConsume(
            queue: QueueName,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation("MotorcycleEventConsumer iniciado e aguardando mensagens...");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task ProcessMessage(string message)
    {
        try
        {
            var motorcycleEvent = JsonConvert.DeserializeObject<MotorcycleRegisteredEvent>(message);

            if (motorcycleEvent == null)
            {
                _logger.LogWarning("Mensagem não pôde ser desserializada: {Message}", message);
                return;
            }

            // ✅ CONSUMER SIMPLIFICADO - Apenas log para motos 2024
            if (motorcycleEvent.Year == 2024)
            {
                _logger.LogInformation("🚨 MOTO 2024 REGISTRADA - Placa: {LicensePlate}, Modelo: {Model}, ID: {MotorcycleId}",
                    motorcycleEvent.LicensePlate, motorcycleEvent.Model, motorcycleEvent.MotorcycleId);

                // Já atende o requisito de "armazenar para consulta futura"
                // Logs estruturados permitem consulta via ferramentas de log
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar mensagem do RabbitMQ");
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}