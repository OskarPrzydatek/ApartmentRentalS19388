using ApartmentRental.Core.Entities;

namespace ApartmentRental.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<int> GetAddressIdOrCreateAsync(string city, string zipCode, string street, string country,
            string buildingNumber,
            string apartmentNumber)
        {
            var id = await _addressRepository.GetAddressIdByItsAttributesAsync(country, city, zipCode, street,
                buildingNumber,
                apartmentNumber);
            if (id != 0)
            {
                return id;
            }

            var address = await _addressRepository.CreateAndGetAsync(new Address
            {
                Country = country,
                City = city,
                ZipCode = zipCode,
                Street = street,
                BuildingNumber = buildingNumber,
                AparmentNumber = apartmentNumber
            });
            return address.Id;
        }
    }
}