using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using IModel = RabbitMQ.Client.IModel;

namespace MotorcycleFlow.Infrastructure.Services
{
    public class RabbitMQNotificationService : INotificationService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly bool _isConnected;

        public RabbitMQNotificationService()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(5)
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _isConnected = true;

                _channel.ExchangeDeclare("motorcycle.events", ExchangeType.Topic, durable: true);
                _channel.ExchangeDeclare("rental.events", ExchangeType.Topic, durable: true);

                Console.WriteLine("✅ RabbitMQ conectado!");
            }
            catch (Exception ex)
            {
                _isConnected = false;
                Console.WriteLine($"⚠️ RabbitMQ não disponível: {ex.Message}");
            }
        }

        public Task PublishMotorcycleRegisteredAsync(Motorcycle motorcycle)
        {
            // ✅ VERIFICAR SE ESTÁ CONECTADO ANTES DE PUBLICAR
            if (!_isConnected)
            {
                Console.WriteLine("⚠️ RabbitMQ offline - mensagem de moto não enviada");
                return Task.CompletedTask;
            }

            try
            {
                var message = new
                {
                    EventType = "MotorcycleRegistered",
                    MotorcycleId = motorcycle.Id,
                    LicensePlate = motorcycle.LicensePlate,
                    Year = motorcycle.Year,
                    Timestamp = DateTime.UtcNow
                };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                _channel.BasicPublish(
                    exchange: "motorcycle.events",
                    routingKey: "motorcycle.registered",
                    basicProperties: null,
                    body: body
                );

                Console.WriteLine($"✅ Mensagem enviada para RabbitMQ: Moto {motorcycle.LicensePlate} registrada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao publicar mensagem no RabbitMQ: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public Task PublishRentalCreatedAsync(Rental rental)
        {
            // ✅ VERIFICAR SE ESTÁ CONECTADO ANTES DE PUBLICAR
            if (!_isConnected)
            {
                Console.WriteLine("⚠️ RabbitMQ offline - mensagem de locação não enviada");
                return Task.CompletedTask;
            }

            try
            {
                var message = new
                {
                    EventType = "RentalCreated",
                    RentalId = rental.Id,
                    DeliveryPersonId = rental.DeliveryPersonId,
                    MotorcycleId = rental.MotorcycleId,
                    TotalCost = rental.TotalCost,
                    Timestamp = DateTime.UtcNow
                };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                _channel.BasicPublish(
                    exchange: "rental.events",
                    routingKey: "rental.created",
                    basicProperties: null,
                    body: body
                );

                Console.WriteLine($"✅ Mensagem enviada para RabbitMQ: Locação {rental.Id} criada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao publicar mensagem no RabbitMQ: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Close();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}