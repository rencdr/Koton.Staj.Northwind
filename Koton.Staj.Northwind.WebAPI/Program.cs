using Koton.Staj.Data.Abstract;
using Koton.Staj.Data.Concrete;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Concrete;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Data.Concrete;
using Microsoft.Extensions.Configuration;




var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var jwtSecretKey = configuration["JwtSecretKey"];

builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository, DapperProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, DapperUserRepository>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICartRepository, DapperCartRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
