using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Enum;
using Accounting.WebAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.IO;
using Accounting.WebAPI.Contracts;


namespace Accounting.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();

            services.ConfigureIISIntegration();

            services.ConfigureLoggerService();

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureSqlContext(Configuration);

            //services.AddDbContext<AccountingContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>(sp =>
             {
                 Data.Tools.Options options =
                     new Data.Tools.Options
                     {
                         Provider =
                             (Provider)Convert.ToInt32(Configuration.GetSection(key: "DatabaseProvider").Value),


                         ConnectionString =
                             Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value,
                     };

                 return new UnitOfWork(options: options);
             });

            services.AddAuthentication();

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            /*It supports versioning so if we have version 1 , version 2 .. we are able to keep trackes of the versions or let whoever reades the documention know which 
            of the api they are looking at*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounting.WebAPI", Version = "v1" });
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            /*env is just a variable that allow s us to track witch enviroment we are in*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "accounting.webapi v1"));

            app.ConfigureExceptionHandler(logger);

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /*Convention based routing */

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name:default,
            //        pattern:"{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
