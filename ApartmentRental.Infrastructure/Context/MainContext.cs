using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Context;

public class MainContext : DbContext
{

    public MainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=dbo.ApartmentRental.db");
    }
}