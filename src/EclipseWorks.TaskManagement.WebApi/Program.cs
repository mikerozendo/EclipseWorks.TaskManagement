using EclipseWorks.TaskManagement.Application.Behavior;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure;
using EclipseWorks.TaskManagement.Infrastructure.Repositories;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environmentConfiguration = builder.Configuration.Get<EnvironmentConfiguration>();
ArgumentNullException.ThrowIfNull(environmentConfiguration);
builder.Services.AddSingleton(environmentConfiguration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IResourceCommandResponse>());


builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<IResourceResponse>();
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResourceResponseValidationBehavior<,>));

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<ITasksHistoryRepository, TasksHistoryRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    builder.WebHost.ConfigureKestrel(serverOptions => { serverOptions.ListenAnyIP(8080); });
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseAuthorization();
app.MapControllers();
app.Run();