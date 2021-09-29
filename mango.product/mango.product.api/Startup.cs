using mango.product.data.Context;
using mango.product.IoC;
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

        }




    }

}
