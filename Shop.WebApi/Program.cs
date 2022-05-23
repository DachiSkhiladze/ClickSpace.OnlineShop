using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Mapper;
using Microsoft.EntityFrameworkCore;
using Shop.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(MappingProfile)); // Injecting AutoMapper Object

builder.Services.ConfigureServicesInjections();

builder
    .Services
    .AddDbContext<OnlineshopContext>(
      x => x.UseSqlServer("Data Source=localhost;Initial Catalog=ClickSpace.ShopOnline;Integrated Security=True")); // Adding DB Context Injection



var app = builder.Build();

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
