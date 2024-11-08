using WorkBlockApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppConfiguration();
builder.Services.AddDIConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.LoadConfiguration(builder.Build());

// builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// builder.Services.AddSingleton<IWorkBlockAppConfiguration, WorkBlockAppConfiguration>();

// builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
// builder.Services.AddScoped<IAdministratorService, AdministratorService>();

// builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
// builder.Services.AddScoped<IEmployerService, EmployerService>();

// builder.Services.AddAutoMapper(typeof(AdministratorMapping));
// builder.Services.AddAutoMapper(typeof(EmployerMapping));


// var app = builder.Build();

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();
// app.MapRazorPages();

// var defaultCulture = new CultureInfo("pt-BR");
// var localizationOptions = new RequestLocalizationOptions
// {
//     DefaultRequestCulture = new RequestCulture(defaultCulture),
//     SupportedCultures = new List<CultureInfo> { defaultCulture },
//     SupportedUICultures = new List<CultureInfo> { defaultCulture }
// };
// app.UseRequestLocalization(localizationOptions);

// app.Run();
