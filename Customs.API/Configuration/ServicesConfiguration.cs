using Customs.DAL;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customs.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<CustomsContext>(x => x.UseSqlServer(connection));

            serviceCollection.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddScoped<IBaseRepository<Product>, ProductRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IBaseRepository<Storage>, StorageRepository>();
            serviceCollection.AddScoped<IBaseRepository<Duty>, DutyRepository>();
        }
    }
}