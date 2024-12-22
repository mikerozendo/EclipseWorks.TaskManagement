using EclipseWorks.TaskManagement.Infrastructure;
using EclipseWorks.TaskManagement.Infrastructure.Repositories;
using EclipseWorks.TaskManagement.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environmentConfiguration = builder.Configuration.Get<EnvironmentConfiguration>();
ArgumentNullException.ThrowIfNull(environmentConfiguration);
builder.Services.AddSingleton(environmentConfiguration);

builder.Services.AddScoped<IRepository<Project>, ProjectsRepository>();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseAuthorization();
app.MapControllers();
app.Run();