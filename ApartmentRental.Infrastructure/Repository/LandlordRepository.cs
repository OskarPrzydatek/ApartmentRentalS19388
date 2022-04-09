using ApartmentRental.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ApartmentRental.Core.Entities;
using ApartmentRental.Infrastructure.Exeptions;

namespace ApartmentRental.Infrastructure.Repository
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly MainContext _mainContext;

        public LandlordRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task AddAsync(Landlord entity)
        {
            var isExist = await (_mainContext.Landlord ?? throw new InvalidOperationException()).AnyAsync(x => x.Id == entity.Id);

            if (isExist)
            {
                throw new Exception("This Landlord already exist");
            }

            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (_mainContext.Landlord == null) throw new EntityNotFoundException();
            var landlordToDelete = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
            if (landlordToDelete == null) throw new EntityNotFoundException();
            _mainContext.Landlord.Remove(landlordToDelete);
            await _mainContext.SaveChangesAsync();

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Landlord>> GetAllAsync()
        {
            var landlord = await (_mainContext.Landlord ?? throw new InvalidOperationException()).ToListAsync();
            return landlord;
        }

        public async Task<Landlord> GetByIdAsync(int id)
        {
            var landlord = await (_mainContext.Landlord ?? throw new InvalidOperationException()).SingleOrDefaultAsync(x => x.Id == id);
            if (landlord == null)
            {
                throw new EntityNotFoundException();
            }

            return landlord;
        }

        public async Task UpdateAsync(Landlord entity)
        {
            var landlordToUpdate = await (_mainContext.Landlord ?? throw new InvalidOperationException()).SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (landlordToUpdate == null)
            {
                throw new EntityNotFoundException();
            }
            landlordToUpdate.Apartments = entity.Apartments;
            landlordToUpdate.AccountId = entity.AccountId;
            landlordToUpdate.Account = entity.Account;
            landlordToUpdate.DateOfUpdate = DateTime.UtcNow;
            await _mainContext.SaveChangesAsync();
        }
    }
}