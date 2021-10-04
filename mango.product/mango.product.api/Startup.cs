using HealthChecks.UI.Client;
using mango.product.api.Health;
using mango.product.data.Context;
using mango.product.IoC;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ordering.api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "mango.product", Version = "v1" });
            });


            //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1
            services.AddHealthChecks()
             .AddCheck<GeneralCheck>(nameof(GeneralCheck))
             .AddCheck<ProductDBCheck>(nameof(ProductDBCheck)); //mi chequeo personalizado
                                                                  //.AddDbContextCheck<OrderContext>(); //chequeo de la base de datos


            DependencyContainer.RegisterServices(services, Configuration);

        }
        //

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ordering.api v1"));

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            /*
            app.UseAuthentication();
            app.UseAuthorization();
            */


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/Checking", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

        }




    }

}
