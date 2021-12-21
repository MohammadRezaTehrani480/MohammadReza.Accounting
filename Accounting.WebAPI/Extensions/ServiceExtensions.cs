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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;

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

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountingContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q => { q.User.RequireUniqueEmail = true; });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AccountingContext>().AddDefaultTokenProviders();
        }
    }
}
