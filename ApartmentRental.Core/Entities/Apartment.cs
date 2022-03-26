namespace ApartmentRental.Core.Entities;

public class Apartment : BaseEntity
{
    public string? RentAmount { get; set; }
    public int NumberOfRooms { get; set; } 
    public int SquareMeters { get; set; }
    public int Floor { get; set; }
    public bool IsElevator { get; set; }
}