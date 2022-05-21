using ApartmentRental.Core.DTO;

namespace ApartmentRental.Core.Services
{
    public interface ILandlordService
    {
        Task AddLandlordAsync(LandlordCreationRequestDto dto);
    }
}