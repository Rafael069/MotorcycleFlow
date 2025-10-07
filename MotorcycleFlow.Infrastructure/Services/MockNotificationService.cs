using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using System;
using System.Threading.Tasks;

namespace MotorcycleFlow.Infrastructure.Services
{
    public class MockNotificationService : INotificationService
    {
        public Task PublishMotorcycleRegisteredAsync(Motorcycle motorcycle)
        {
            Console.WriteLine($"📨 [MOCK] Motorcycle {motorcycle.LicensePlate} registered - Event would be sent to RabbitMQ");
            return Task.CompletedTask;
        }

        public Task PublishRentalCreatedAsync(Rental rental)
        {
            Console.WriteLine($"📨 [MOCK] Rental {rental.Id} created - Event would be sent to RabbitMQ");
            return Task.CompletedTask;
        }
    }
}
