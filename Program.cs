using ApplicationLayer.Service.TareaService;
using Domainlayer.Models;
using InfraestuctureLayer;
using InfraestuctureLayer.Repositore.Commons;
using InfraestuctureLayer.Repositore.RepositoreTask;
using Microsoft.EntityFrameworkCore;
using ApplicationLayer.Queue;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<TaskManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("taskmanager"));
});

// Registro de servicios
builder.Services.AddScoped<IcommonsProcess<Tareas>, TaskRepositore>();
builder.Services.AddScoped<TaskRepositore>();
builder.Services.AddScoped<ITaskFactory, TareaFactory>();
builder.Services.AddSingleton<TaskQueueProcessor>();
builder.Services.AddScoped<TaskService>();

// Configuración de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones pendientes
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
    dbContext.Database.Migrate();
}

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
