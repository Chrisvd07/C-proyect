using ApplicationLayer.Service.TareaService;
using Domainlayer.Models;
using InfraestuctureLayer;
using InfraestuctureLayer.Repositore.Commons;
using InfraestuctureLayer.Repositore.RepositoreTask;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TaskManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("taskmanager"));
});

builder.Services.AddScoped<IcommonsProcess<Tareas>, TaskRepositore>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ITaskFactory, ApplicationLayer.Service.TareaService.TareaFactory>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();