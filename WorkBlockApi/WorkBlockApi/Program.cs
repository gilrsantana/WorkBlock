using WorkBlockApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder);
builder.Services.AddContext(builder);
builder.Services.AddRepositories();

builder.Services.LoadConfiguration(builder.Build());