using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.ProductServices;
using ProductCategory.Service.Profiles;
using ProductCategory.Services.ProductServices;

namespace ProductCategorySolution
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductCategorySolution", Version = "v1" });
            });

            services.AddHttpClient();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDataDao<Product>, ProductDao>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDataDao<Category>, CategoryDao>();

            services.AddAutoMapper(typeof(ProductCategoryProfile));

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Configuration.GetConnectionString("MongoDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCategorySolution v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
