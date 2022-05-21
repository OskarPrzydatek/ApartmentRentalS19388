namespace ApartmentRental.Core.DTO
{
    public class ApartmentBasicInformationResponseDto
    {
        public decimal RentAmount { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal SquareMeters { get; set; }
        public bool IsElevatorInBuilding { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public ApartmentBasicInformationResponseDto(decimal rentAmount, int numberOdRooms, decimal squareMeters,
            bool isElevatorInBuilding, string city, string street)
        {
            RentAmount = rentAmount;
            NumberOfRooms = numberOdRooms;
            SquareMeters = squareMeters;
            IsElevatorInBuilding = isElevatorInBuilding;
            City = city;
            Street = street;
        }
    }
}