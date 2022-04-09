using ApartmentRental.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ApartmentRental.Core.Entities;
using ApartmentRental.Infrastructure.Exeptions;

namespace ApartmentRental.Infrastructure.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MainContext _mainContext;

        public AddressRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task AddAsync(Address entity)
        {
            var isExist = _mainContext.Address != null && await _mainContext.Address.AnyAsync(x =>
                x.City == entity.City
                && x.Street == entity.Street
                && x.BuildingNumber == entity.BuildingNumber
                && x.AparmentNumber == entity.AparmentNumber
                && x.ZipCode == entity.ZipCode
                && x.Country == entity.Country);

            if (isExist)
            {
                throw new Exception("Address already exist");
            }

            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (_mainContext.Address == null) throw new EntityNotFoundException();
            var addressToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
            if (addressToDelete == null) throw new EntityNotFoundException();
            _mainContext.Address.Remove(addressToDelete);
            await _mainContext.SaveChangesAsync();

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var address = await (_mainContext.Address ?? throw new InvalidOperationException()).ToListAsync();

            return address;
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var address =
                await (_mainContext.Address ?? throw new InvalidOperationException()).SingleOrDefaultAsync(x =>
                    x.Id == id);
            if (address == null)
            {
                throw new EntityNotFoundException();
            }

            return address;
        }

        public async Task UpdateAsync(Address entity)
        {
            if (_mainContext.Address != null)
            {
                var addressToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);
                if (addressToUpdate == null)
                {
                    throw new EntityNotFoundException();
                }

                addressToUpdate.Street = entity.Street;
                addressToUpdate.AparmentNumber = entity.AparmentNumber;
                addressToUpdate.BuildingNumber = entity.BuildingNumber;
                addressToUpdate.City = entity.City;
                addressToUpdate.ZipCode = entity.ZipCode;
                addressToUpdate.Country = entity.Country;
                addressToUpdate.DateOfUpdate = DateTime.UtcNow;
            }

            await _mainContext.SaveChangesAsync();
        }
    }
}