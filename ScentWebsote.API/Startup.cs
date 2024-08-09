using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ScentWebsote.API.Data;
using Microsoft.OpenApi.Models;
using ScentWebsote.API.Mappings;
using ScentWebsote.API.Data.Interfaces;


namespace ScentWebsite.API
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

           //services.AddControllers().AddJsonOptions(options =>
           // {
           //     options.JsonSerializerOptions.PropertyNamingPolicy = null;
           //     options.JsonSerializerOptions.DictionaryKeyPolicy = null;
           //     options.JsonSerializerOptions.IgnoreNullValues = true;
           // });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                builder => builder
             .WithOrigins("http://localhost:3000") // Adjust with your React app's URL
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowReactApp");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
