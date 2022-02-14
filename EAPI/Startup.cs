using EAPI.Behaviour;
using EAPI.Behaviour.Interfaces;
using EAPI.DataAccess;
using EAPI.DataAccess.DataAccess;
using EAPI.DataAccess.DataAccess.Interfaces;
using EAPI.DataAccess.Entities;
using EAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen();
            RegisterBehaviours(services);
            RegisterDataAccessLayers(services);
            string conection=  Configuration.GetSection("HostSettings").GetValue<string>("ConnectionString");
            services.AddDbContext<EAPIContext>(options => options.UseSqlServer(conection));
        }

        private static void RegisterBehaviours(IServiceCollection services)
        {
            services.AddScoped<IUserControlBehaviour, UserControlBehaviour>();
            services.AddScoped<IOrderBehaviour, OrderBehavior>();
        }
        private static void RegisterDataAccessLayers(IServiceCollection services)
        {
            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IOrderDataAccess, OrderDataAccess>();
            services.AddScoped<IProductsDataAccess, ProductDataAccess>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
