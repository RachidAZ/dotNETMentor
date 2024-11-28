using Asp.Versioning;
using CartService.BLL;
using CartService.BLL.Entities;
using CartService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// api versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader());
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add EF
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseMongoDB(builder.Configuration.GetConnectionString("CartService_mongoDB"), "NETMentor"));






// add repository DI
builder.Services.AddScoped<IRepository<Cart, Guid>, RepositoryCart>();
builder.Services.AddScoped<IRepository<Item, int>, Repository<Item, int>>();
builder.Services.AddScoped<ICartService, CartService.BLL.CartService>();


// add background service to listen to RabbitMQ events
builder.Services.AddHostedService<CartBackgroundService>();


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
