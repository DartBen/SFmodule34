using HomeApi.Configuration;
using HomeApi.Contracts.Validation;
using FluentValidation;
using HomeApi.Contracts.Devices;
using FluentValidation.AspNetCore;

namespace HomeApi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("Configuration\\HomeOptions.json");

            // Add services to the container.
            // Добавляем новый сервис
            builder.Services.Configure<HomeOptions>(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Подключаем валидацию
            builder.Services.AddFluentValidation(fv => 
                fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>());

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

            app.Run();
        }
    }
}