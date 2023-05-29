using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using Application.Activities;
using Application.Core;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();sql

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
    await Seed.SeedData(context);
}
catch (System.Exception ex)
{
   var logger = services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex, "An error occurred during migration.");
}

app.Run();
