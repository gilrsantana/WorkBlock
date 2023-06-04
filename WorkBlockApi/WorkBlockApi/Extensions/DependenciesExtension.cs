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
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        
        services.AddCors(options => 
        {
            options.AddPolicy("Production",
            policy => 
            {
                policy.WithOrigins("https://pontoblock.gilmarsantana.com/", "http://pontoblock.gilmarsantana.com/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            
        });
    }

    public static void LoadConfiguration(this IServiceCollection services, WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseCors();
        }

        if (app.Environment.IsProduction())
        {
            app.UseCors("Production");
        }
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}