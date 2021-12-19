using Accounting.WebAPI.Data;
using Accounting.WebAPI.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Contracts;
using Accounting.WebAPI.LoggerService;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader())
             );
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddTransient<Data.IUnitOfWork, Data.UnitOfWork>(sp =>
        //    {
        //        Data.Tools.Options options =
        //            new Data.Tools.Options
        //            {
        //                Provider =
        //                    (Provider)Convert.ToInt32(configuration.GetSection(key: "databaseProvider").Value),


        //                ConnectionString =
        //                    configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value,
        //            };

        //        return new UnitOfWork(options: options);
        //    });
        //}

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountingContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
        }
    }
}
