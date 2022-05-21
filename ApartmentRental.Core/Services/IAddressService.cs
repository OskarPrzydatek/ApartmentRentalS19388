namespace ApartmentRental.Core.Services
{
    public interface IAddressService
    {
        public Task<int> GetAddressIdOrCreateAsync(string country, string city, string zipCode, string street,
            string buildingNumber, string apartmentNumber);
    }
}