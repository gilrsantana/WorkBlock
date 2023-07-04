using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Data;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Repositories;

namespace WorkBlockApi.Extensions;

public static class DependenciesExtension
{
    public static void AddApiConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var apiConfiguration = new ApiConfiguration
        {
            PrivateKey = builder.Configuration.GetValue<string>("PrivateKey") ?? "",
            Provider = builder.Configuration.GetValue<string>("Provider") ?? "",
            AdminAddress = builder.Configuration.GetValue<string>("AdminAddress") ?? ""
        };
        services.AddSingleton(apiConfiguration);
        services.AddMemoryCache();
        services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }

    public static void AddContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<WorkBlockContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IContractModelRepository, ContractModelRepository>();
    }

    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Development",
            policy =>
            {
                policy.WithOrigins("http://localhost:5500", "https://localhost:50630")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

        });
        services.AddCors(options =>
        {
            options.AddPolicy("Production",
            policy =>
            {
                policy.WithOrigins("https://pontoblock.gilmarsantana.com/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

        });
    }

    public static void LoadConfiguration(this IServiceCollection services, WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            System.Console.WriteLine("Development");
            
            app.UseCors();
        }

        if (app.Environment.IsProduction())
        {
            System.Console.WriteLine("Production");
            app.UseCors("Production");
        }

        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}