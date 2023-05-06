using Microsoft.EntityFrameworkCore;
using WorkBlockApi;
using WorkBlockApi.Data;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiConfiguration = new ApiConfiguration
    {
        PrivateKey = builder.Configuration.GetValue<string>("PrivateKey") ?? "",
        Provider = builder.Configuration.GetValue<string>("Provider") ?? ""
    };

builder.Services.AddTransient<IContractModelRepository, ContractModelRepository>();
builder.Services.AddSingleton(apiConfiguration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WorkBlockContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
