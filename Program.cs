using crud_curotec.Application.Services;
using crud_curotec.Application.Validators;
using crud_curotec.Configuration;
using crud_curotec.Domain.Entities;
using crud_curotec.Domain.Interfaces.Repository;
using crud_curotec.Domain.Interfaces.Services;
using crud_curotec.Infraestructure.Data;
using crud_curotec.Infraestructure.Repository;
using crud_curotec.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace crud_curotec
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<AppSettings>(
            builder.Configuration.GetSection("AppSettings"));
            var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(appSettings!.DatabaseConnectionString));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

            builder.Services.AddSwaggerGen(c =>
            {
               
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "Api CRUD for Curotec assessment",
                    Description = "Endpoint with CRUD operations.",
                    Contact = new OpenApiContact
                    {
                        Name = "Wendel Viana",
                        Email = "wendelviana@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/wendel-viana-wvp/?locale=en_US"),
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
