namespace ApartmentRental.Core.DTO
{
    public class LandlordCreationRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string NumberOfBuilding { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}