using APITeaching_172100251.Data;
using APITeaching_172100251.Mapping;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using APITeaching_172100251.Repositories.SecondApproach;
using APITeaching_172100251.Repositories.Simple;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APITeaching_172100251.Data;
using APITeaching_172100251.Mapping;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using APITeaching_172100251.Repositories.SecondApproach;
using APITeaching_172100251.Repositories.Simple;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<ApiteachingContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShipperService, ShipperService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IService<Country>, CountryService>();
builder.Services.AddScoped<IService<Province>, ProvinceService>();
builder.Services.AddScoped<IService<Address>, AddressService>();
builder.Services.AddScoped<IService<District>, DistrictService>();
builder.Services.AddScoped<IService<Ward>, WardService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
