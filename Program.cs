using CodeChallenge.Controllers;
using CodeChallenge.Interfaces;
using CodeChallenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CarPriceContext>(opt =>
    opt.UseInMemoryDatabase("CarPriceContext"));

builder.Services.AddScoped<ISeedData, SeedDataService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Init data for local testing
 var context = app.Services.CreateScope().ServiceProvider.GetService<CarPriceContext>();
 context.CarPrices.Add(new CarPrice()
 {
             Id = 9,
             Make = "Toyota",
             Model = "4Runner"
         });
 context.SaveChanges();
 #endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
