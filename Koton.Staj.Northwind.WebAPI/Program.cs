using FluentAssertions.Common;
using FluentValidation;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Data.Concrete;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Concrete;
using Koton.Staj.Northwind.Business.Mapper;
using Koton.Staj.Northwind.Business.Validation;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Data.Concrete;
using Koton.Staj.Northwind.Entities.Concrete;
using Koton.Staj.Northwind.Entities.Dtos;

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
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IUserOrderRepository, DapperUserOrderRepository>();



builder.Services.AddTransient<IValidator<User>, UserValidator>();
builder.Services.AddTransient<IValidator<AddToCartDto>, AddToCartDtoValidator>();
builder.Services.AddTransient<IValidator<OrderRequestModel>, CreateOrderValidator>();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(AddToCartDtoValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateOrderValidator));





// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(CartProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(typeof(ProductProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors Con
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Cors Con
app.UseCors(options =>
{
    options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
