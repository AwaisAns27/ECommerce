using ECommerce.DAL.Data;
using ECommerce.DAL.Services.Implimentation;
using ECommerce.DAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web
{
    public static class ConfigurationServices
    {
        /// <summary>
        /// Register your Dependencies 
        /// </summary>
        /// <param name="services">builder.Services</param>
        /// <param name="configuration">builder.Configuration</param>
        public static void RegisterDependencies(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DbConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            //builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

            //builder.Services.AddScoped<IProductRepo, ProductRepo>();

           builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
