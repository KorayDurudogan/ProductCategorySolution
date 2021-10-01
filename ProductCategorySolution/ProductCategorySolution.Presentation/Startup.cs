using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Infrastructure.Redis;
using ProductCategory.Service;
using ProductCategory.Service.Auth;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.ProductServices;
using ProductCategory.Service.Profiles;
using ProductCategory.Services.ProductServices;
using ProductCategorySolution.Presentation.ExceptionHandling;
using ProductCategorySolution.Presentation.ExceptionHandling.Factory;
using System.Text;

namespace ProductCategorySolution
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddRedis($"{Configuration.GetSection(DatabaseConstants.RedisEndpoint).Value},abortConnect=false", DatabaseConstants.CacheName, HealthStatus.Degraded, tags: new[] { "redis", "cache" })
                .AddMongoDb(Configuration.GetConnectionString(DatabaseConstants.DatabaseName), DatabaseConstants.DatabaseName, HealthStatus.Degraded, tags: new[] { "mongo-db", "database" });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductCategorySolution", Version = "v1" });
            });

            services.AddHttpClient();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDataDao<Product>, ProductDao>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDataDao<Category>, CategoryDao>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton<IExceptionHandlerFactory, ExceptionHandlerFactory>();

            services.AddSingleton<IRedisManager, RedisManager>();

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Configuration.GetConnectionString(DatabaseConstants.DatabaseName)));

            services.AddAutoMapper(typeof(ProductCategoryProfile));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection(TokenConstants.TokenIssuer).Value,
                        ValidAudience = Configuration.GetSection(TokenConstants.TokenAudience).Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection(TokenConstants.TokenSigningKey).Value))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRedisManager redisManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCategorySolution v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            redisManager.Connect();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks(GeneralConstants.HealthcheckEndpoint, new HealthCheckOptions()
            {
                Predicate = registration => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks(GeneralConstants.HealthcheckEndpoint);
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
