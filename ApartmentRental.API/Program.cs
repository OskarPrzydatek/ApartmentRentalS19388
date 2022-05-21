using ApartmentRental.Core.Services;
using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ILandLordService, LandLordService>();
builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlite("DataSource=dbo.ApartmentRental.db",
        sqlOptions => sqlOptions.MigrationsAssembly("ApartmentRental.Infrastructure")
    )
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();