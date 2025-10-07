
using MediatR;
using Microsoft.EntityFrameworkCore;
using MotorcycleFlow.Application.Features.Motorcycles.Commands;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Application.Services;
using MotorcycleFlow.Infrastructure.Data;
using MotorcycleFlow.Infrastructure.Repositories;
using MotorcycleFlow.Infrastructure.Services;


namespace MotorcycleFlow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

            builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            builder.Services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            builder.Services.AddScoped<IRentalCalculatorService, RentalCalculatorService>();
            builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();
            builder.Services.AddScoped<INotificationService, RabbitMQNotificationService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(typeof(CreateMotorcycleCommand));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
