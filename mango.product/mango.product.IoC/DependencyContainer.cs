
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using mango.product.data.Context;
using MediatR;
using store.dal.DataConnections;
using store.dal.DataConnections.Sql;
using mango.product.domain.Interfaces;
using mango.product.data.Repositories;
using mango.product.application.Interfaces;
using mango.product.application.Services;
using mango.product.DAL.Services;
using mango.product.DAL.Interfaces;

namespace mango.product.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            //ProductContext (data base)
            services.AddDbContext<ProductContext>(c =>
                c.UseSqlServer(configuration["ConnectionStrings:MangoProductDB"]));// b => b.MigrationsAssembly("mango.product.api")) ; , ServiceLifetime.Singleton);

            //DAL connections
            services.AddTransient<IDatabaseBuilder>(provider => new SqlDatabaseBuilder(configuration["ConnectionStrings:MangoProductDB"]));

            //Repositories
            services.AddTransient<IProductsRepository, ProductRepository>();

            //AutoMapper
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.Where(ass => ass.FullName != null && ass.FullName.Contains("mango.product.")).ToArray();
            services.AddAutoMapper(assemblies);


            //mediatR. store.DAL
            assembly = assemblies.Where(ass => ass.FullName != null && ass.FullName.Contains("mango.product.")).ToArray();
            services.AddMediatR(assembly);



            //Services
            //Services application
            services.AddTransient<IProductsService, ProductService>();


            //Services DAL
            services.AddTransient<IProductsServiceDal, ProductsServiceDal>();

        }
    }
}
