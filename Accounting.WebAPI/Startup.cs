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
using System.IO;
using Accounting.WebAPI.Mappings;
using Accounting.WebAPI.Services;
using Marvin.Cache.Headers;
using AspNetCoreRateLimit;

namespace Accounting.WebAPI
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
            services.ConfigureSqlContext(Configuration);

            /*Cashing on our Api can significantly improve the performance of Api.When we have hundredes of clients subscribe
            to our Api they are all trying to find realpeople and etc.that can realy take toll on the performance of Api based on the whole
            infrastructure.So cashing will interduce kind of quick access layer on top of the real data store and it can signifcantly reduce
            how we often we have to pull database.*/
            services.AddMemoryCache();

            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();

            services.AddResponseCaching();

            services.AddAuthentication();

            services.ConfigureIdentity();

            services.ConfigureJWT(Configuration);

            services.ConfigureCors();

            services.ConfigureIISIntegration();

            services.AddAutoMapper(typeof(MapperInitilizer));

            //services.AddTransient<IUnitOfWork, UnitOfWork>();

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

            services.AddScoped<IAuthManager, AuthManager>();


            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            /*It supports versioning so if we have version 1 , version 2 .. we are able to keep trackes of the versions or let whoever reades the documention know which 
            of the api they are looking at*/
            services.ConfigureSwagger();

            /*We are basically saying when you notice the reference loop happening , do not make big deal out of just
             ignore it and let program run*/
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
            }).AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.ConfigureVersioning();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*env is just a variable that allow s us to track witch enviroment we are in*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(); app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounting.WebAPI v1");
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "Accounting.WebAPI v2");
            });

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseIpRateLimiting();

            app.UseRouting();

            app.UseAuthentication();
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
