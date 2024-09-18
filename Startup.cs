using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DotnetBakery.Models;
using System;
using Microsoft.OpenApi.Models;

namespace DotnetBakery
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
            // Add framework services.
            // see if we have an environment variable
            string DATABASE_URL = Environment.GetEnvironmentVariable("DATABASE_URL_STR");
            string connectionString = (DATABASE_URL == null ? Configuration.GetConnectionString("DefaultConnection") : DATABASE_URL);
            Console.WriteLine($"Using connection string: {connectionString}");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionString)
            );

            // Added these to call the swagger ui
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Da Bakery API",
                    Description = "Da Bakers bake Da Bread.",
                });

                // using System.Reflection;
                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddMvc();

            // In production, the React files will be served from this directory

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                // Tell the app to use swagger
                app.UseSwagger();
                // Tell the app to use the swagger ui.
                // Will be located at /swagger
                app.UseSwaggerUI(c =>
                  {
                      // This tells swagger where the json will be built for it
                      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Da Bakery v1");
                  });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
            //         // spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }
    }
}
