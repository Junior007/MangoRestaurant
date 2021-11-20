using mango.cart.data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mango.cart.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            //ProductContext (data base)
            services.AddDbContext<CartContext>(c =>
                c.UseSqlServer(configuration["ConnectionStrings:MangoProductDB"]));// b => b.MigrationsAssembly("mango.product.api")) ; , ServiceLifetime.Singleton);
            /*
            //DAL connections
            services.AddTransient<IDatabaseBuilder>(provider => new SqlDatabaseBuilder(configuration["ConnectionStrings:MangoProductDB"]));

            //Repositories
            services.AddTransient<IProductsRepository, ProductRepository>();
            */
            //AutoMapper
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.Where(ass => ass.FullName != null && ass.FullName.Contains("mango.cart.")).ToArray();
            services.AddAutoMapper(assemblies);

            /*
            //mediatR. store.DAL
            assembly = assemblies.Where(ass => ass.FullName != null && ass.FullName.Contains("mango.product.")).ToArray();
            services.AddMediatR(assembly);



            //Services
            //Services application
            services.AddTransient<IProductsService, ProductService>();


            //Services DAL
            services.AddTransient<IProductsServiceDal, ProductsServiceDal>();*/

        }
    }
}
