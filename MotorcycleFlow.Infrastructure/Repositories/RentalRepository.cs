using Microsoft.EntityFrameworkCore;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Core.Enums;
using MotorcycleFlow.Infrastructure.Data;

namespace MotorcycleFlow.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> GetByIdAsync(string id)
        {
            return await _context.Rentals
                .Include(r => r.DeliveryPerson)
                .Include(r => r.Motorcycle)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Rental>> GetActiveRentalsAsync(string motorcycleId)
        {
            return await _context.Rentals
                .Where(r => r.MotorcycleId == motorcycleId && r.Status == RentalStatusEnum.Active)
                .ToListAsync();
        }

        public async Task<bool> HasActiveRentalsAsync(string motorcycleId)
        {
            return await _context.Rentals
                .AnyAsync(r => r.MotorcycleId == motorcycleId && r.Status == RentalStatusEnum.Active);
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }
    }
}