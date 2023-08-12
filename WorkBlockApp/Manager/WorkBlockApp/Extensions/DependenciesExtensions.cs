using System.Globalization;
using Microsoft.AspNetCore.Localization;
using WorkBlockApp.Interfaces.IEnvironment;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Mappings;
using WorkBlockApp.Models.Environment;
using WorkBlockApp.Repository;
using WorkBlockApp.Rest;
using WorkBlockApp.Services;

namespace WorkBlockApp.Extensions;

public static class DependenciesExtensions
{
    public static void AddAppConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IWorkBlockAppConfiguration, WorkBlockAppConfiguration>();
        services.AddRazorPages().AddRazorRuntimeCompilation();
    }

    public static void AddDIConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<IAdministratorService, AdministratorService>();

        services.AddScoped<IEmployerRepository, EmployerRepository>();
        services.AddScoped<IEmployerService, EmployerService>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IPontoBlockReportsService, ReportsService>();
        services.AddScoped<IPontoBlockReportsRepository, ReportsRepository>();
    }

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AdministratorMapping));
        services.AddAutoMapper(typeof(EmployerMapping));
    }

    private static RequestLocalizationOptions GetLocalizationOptions()
    {
        var defaultCulture = new CultureInfo("pt-BR");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };

        return localizationOptions;
    }

    public static void LoadConfiguration(this IServiceCollection services, WebApplication app)
    {
        app.UseRequestLocalization(GetLocalizationOptions());

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapRazorPages();

        app.Run();
    }
}
