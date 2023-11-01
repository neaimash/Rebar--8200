using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Rebar.Models;
using Rebar.Data;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Rebar.Entities;
using Rebar.Model;

var builder = WebApplication.CreateBuilder(args);
// Add HttpClient registration
builder.Services.AddHttpClient();
// Register services here
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));

builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoDBContext(settings.ConnectionString, settings.DatabaseName);
});
builder.Services.AddScoped<Menu>();  // Register the Menu service

// Add controllers
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Rebar:8200API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rebar:8200API v1"));
app.UseAuthorization();

app.MapControllers();

app.Run();