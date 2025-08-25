using Microsoft.EntityFrameworkCore;
using PharmaControl.API.Configurations;
using PharmaControl.Application.Interfaces;
using PharmaControl.Application.Interfaces.Employee;
using PharmaControl.Application.Interfaces.Supplier;
using PharmaControl.Application.Services.Employee;
using PharmaControl.Application.Services.Supplier;
using PharmaControl.Domain.Models;
using PharmaControl.Infrastructure.DataContext;
using PharmaControl.Infrastructure.Repositories;
using PharmaControl.Infrastructure.Repositories.Supplier;

var builder = WebApplication.CreateBuilder(args);

//DatabaseConnection
builder.Services.AddDbContext<PharmaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//DependencyInjections
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

//Mapping
builder.Services.AddAutoMapper(typeof(MappingProfileConfigurations));

//Swagger
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();