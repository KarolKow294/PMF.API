
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMF.API.Entities;
using PMF.API.Models;
using PMF.API.Models.Validators;
using PMF.API.Services;
using System.Reflection;

namespace PMF.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers().AddFluentValidation();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<CsvService>();
            builder.Services.AddSingleton<QrCodeService>();
            builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderMappingProfile(provider.GetService<QrCodeService>()));
            }).CreateMapper());
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", policyBuilder =>
                policyBuilder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(builder.Configuration["AllowedOrigins"])
                );
            });

            builder.Services.AddDbContext<PmfDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("PmfDbConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("FrontEndClient");

            app.Run();
        }
    }
}
