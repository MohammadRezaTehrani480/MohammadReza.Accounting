using Accounting.WebAPI.Data;
using Accounting.WebAPI.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Marvin.Cache.Headers;
using AspNetCoreRateLimit;

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
            /*With the AddIdentityCore method, we are adding and configuring Identity for the specific type; in this case, the ApiUser type.*/
            var builder = services.AddIdentityCore<ApiUser>(q =>
            {
                q.User.RequireUniqueEmail = true;
                q.Password.RequireDigit = true;
                q.Password.RequireLowercase = false;
                q.Password.RequireUppercase = false;
                q.Password.RequireNonAlphanumeric = false;
                q.Password.RequiredLength = 10;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AccountingContext>().AddDefaultTokenProviders();
        }

        //Iconfiguration gives us access to appsetting.josn configuration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("Key");

            services.AddAuthentication(options =>
            {
                /*This basically saying that we are adding authentication to the application and the defualt scheme
                  that I want is jwt*/
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    /*Validate who issuer a token so  when some body put another value which is opposed to what we have 
                      specified in appsetting.json it means it is invalid.*/
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    /*This will reject the token if it is expired.*/
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "applicaton/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong in the {nameof(contextFeature.Error)}");
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Messege = "Internal Server Error! Please Try Again Later."
                        }.ToString());
                    }
                });
            });
        }

        /*The reason that we want to version our api is that after a period of time more functionality will envolve
        there are time that we may change or the way our endpoints behavior may change and then one day 
        all of your clients wake up and realize they have to change the entire code base because we have implemented
        a change in our api.So versioning an api allows us to kind of run parallel between the old way of doing things
        and the new way of doing things and eventually we can phase out the one that all clients have*/
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                /*Means that there will be a header in our response that will be see and saying that this is the
                  the version that you are using */
                opt.ReportApiVersions = true;
                /*Means if we have one, two or three Api versions and the client failed to specify that they want to use
                 version one, two or three then we are just going to use default one.*/
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounting.WebAPI", Version = "v1" });
                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Accounting.WebAPI", Version = "v2" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {

                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                         new List<string>()
                    }
                });
            });
        }


        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(
            (expirationOpt) =>
            {
                expirationOpt.MaxAge = 120;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            });
        }


        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
              new RateLimitRule
              {
                  Endpoint = "*",
                  Limit = 3,
                  Period = "5m"
              }
            };
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }
    }
}

