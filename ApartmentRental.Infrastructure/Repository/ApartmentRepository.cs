using ApartmentRental.Core.Entities;
using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class ApartmentRepository : IApartmentRepository
{
    private readonly MainContext _mainContext;

    public ApartmentRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Apartment>> GetAllAsync()
    {
        var apartments = await (_mainContext.Apartment ?? throw new InvalidOperationException()).ToListAsync();
        foreach (var apartment in apartments)
        {
            await _mainContext.Entry(apartment).Reference(x => x.Address).LoadAsync();
        }

        return apartments;
    }

    public async Task<Apartment> GetByIdAsync(int id)
    {
        if (_mainContext.Apartment == null) throw new EntityNotFoundException();
        var apartment = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);
        if (apartment == null) throw new EntityNotFoundException();
        {
            await _mainContext.Entry(apartment).Reference(x => x.Address).LoadAsync();
            return apartment;
        }
    }

    public async Task AddAsync(Apartment entity)
    {
        var isExist = _mainContext.Apartment != null && await _mainContext.Apartment.AnyAsync(x =>
            x.Address.City == entity.Address.City
            && x.Address.Street == entity.Address.Street
            && x.Address.BuildingNumber == entity.Address.BuildingNumber
            && x.Address.ZipCode == entity.Address.ZipCode
            && x.Address.Country == entity.Address.Country
            && x.Address.AparmentNumber == entity.Address.AparmentNumber);

        if (isExist)
        {
            throw new Exception("Apartment doesn't Exist");
        }

        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Apartment entity)
    {
        if (_mainContext.Apartment != null)
        {
            var apartmentToUpdate = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (apartmentToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            apartmentToUpdate.Floor = entity.Floor;
            apartmentToUpdate.IsElevator = entity.IsElevator;
            apartmentToUpdate.RentAmount = entity.RentAmount;
            apartmentToUpdate.SquareMeters = entity.SquareMeters;
            apartmentToUpdate.NumberOfRooms = entity.NumberOfRooms;
            apartmentToUpdate.DateOfUpdate = DateTime.UtcNow;
        }

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        if (_mainContext.Apartment != null)
        {
            var apartmentToDelete = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);
            if (apartmentToDelete == null) throw new EntityNotFoundException();
            _mainContext.Apartment.Remove(apartmentToDelete);
        }

        await _mainContext.SaveChangesAsync();

        throw new EntityNotFoundException();
    }
}