
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using mango.product.data.Context;

namespace mango.product.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            //ProductContext (data base)
            services.AddDbContext<ProductContext>(c =>
                c.UseSqlServer(configuration["ConnectionStrings:MangoProductDB"]));// b => b.MigrationsAssembly("mango.product.api")) ; , ServiceLifetime.Singleton);

            //AutoMapper
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.Where(ass => ass.FullName != null && ass.FullName.Contains("mango.product.")).ToArray();

            services.AddAutoMapper(assemblies);

        }
    }
}
