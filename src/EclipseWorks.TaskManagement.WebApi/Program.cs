using EclipseWorks.TaskManagement.Infrastructure;
using EclipseWorks.TaskManagement.Infrastructure.Repositories;
using EclipseWorks.TaskManagement.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(() =>
    builder.Configuration.Get<EnvironmentConfiguration>()
);

builder.Services.AddScoped<IRepository<Project>, ProjectsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();