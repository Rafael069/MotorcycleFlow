using Microsoft.EntityFrameworkCore;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Infrastructure.Data;

namespace MotorcycleFlow.Infrastructure.Repositories
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryPerson> GetByIdAsync(string id)
        {
            return await _context.DeliveryPeople
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DeliveryPerson> GetByCnpjAsync(string cnpj)
        {
            return await _context.DeliveryPeople
                .FirstOrDefaultAsync(d => d.CNPJ == cnpj);
        }

        public async Task<DeliveryPerson> GetByDriverLicenseNumberAsync(string driverLicenseNumber)
        {
            return await _context.DeliveryPeople
                .FirstOrDefaultAsync(d => d.DriverLicenseNumber == driverLicenseNumber);
        }

        public async Task AddAsync(DeliveryPerson deliveryPerson)
        {
            await _context.DeliveryPeople.AddAsync(deliveryPerson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DeliveryPerson deliveryPerson)
        {
            _context.DeliveryPeople.Update(deliveryPerson);
            await _context.SaveChangesAsync();
        }
    }
}