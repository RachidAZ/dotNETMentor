using e_commerce.BLL;
using e_commerce.BLL.Entities;
using e_commerce.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NETMentor.DAL;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add EF
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseMongoDB(builder.Configuration.GetConnectionString("NETMentor_mongoDB"), "NETMentor"));






// add repository DI
builder.Services.AddScoped<IRepository<Cart, Guid>, Repository<Cart, Guid>>();
builder.Services.AddScoped<IRepository<Item, int>, Repository<Item, int>>();
builder.Services.AddScoped<ICartService, CartService>();

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
