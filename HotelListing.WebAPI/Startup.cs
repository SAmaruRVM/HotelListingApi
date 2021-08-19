using HotelListing.WebAPI.Configurations;
using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;
using HotelListing.WebAPI.Repository;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HotelListing.WebAPI
{
    public class Startup
    {
        private const string ALLOW_ALL_CORS_POLICY = "AllowAllCorsPolicy";
        public Startup(IConfiguration configuration) => (Configuration) = (configuration);

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddCors(cors =>
            {
                cors.AddPolicy(name: ALLOW_ALL_CORS_POLICY, configurePolicy: corsPolicy =>
                {
                    corsPolicy.AllowAnyHeader()
                              .AllowAnyOrigin()
                              .AllowAnyMethod();
                });
            });

            services.AddRouting(routingOptions =>
            {
                routingOptions.AppendTrailingSlash = true;
                routingOptions.LowercaseQueryStrings = true;
                routingOptions.LowercaseUrls = true;
            });

            services.AddAutoMapper(typeof(MapperInitializer));

            services.AddDbContextPool<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQL-SERVER")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HotelListing.WebAPI",
                    Version = "v1",
                    Description = "Descrição do meu projeto",
                    Contact = new OpenApiContact
                    {
                        Email = "joaopedromane23@gmail.com",
                        Name = "Grande Escolha"
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(ALLOW_ALL_CORS_POLICY);


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing.WebAPI v1"));

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
