using MemoryManagingTest.BGServices;
using MemoryManagingTest.Views;
using MemoryManagingTest.Views.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDataItemPool, DataItemPool>();
builder.Services.AddHostedService(_ => new GcTriggerService(
    50*1024*1024,
    TimeSpan.FromSeconds(20)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger API" ,
        Version = "v1",
        Description = "Demo for memmory managing"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger API Demo v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();