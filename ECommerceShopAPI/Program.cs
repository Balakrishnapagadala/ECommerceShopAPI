using ECommerceShopAPI.Common;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Repository;
using ECommerceShopAPI.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var commandAssembly = AppDomain.CurrentDomain.Load("ECommerceShopAPI.Command");
var queryAssembly = AppDomain.CurrentDomain.Load("ECommerceShopAPI.Queries");
builder.Services.AddTransient<IECommerceShopRepository, ECommerceShopRepository>();
builder.Services.AddTransient<IECommerceShopService, ECommerceShopService>();
builder.Services.AddTransient<EcommerceShopDbContext>();
var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
Assembly[] assemblies = {queryAssembly, commandAssembly };
builder.Services.AddMediatR(assemblies);
builder.Services.AddAutoMapper(typeof(MapperProfile));

Log.Logger = new LoggerConfiguration().WriteTo.MSSqlServer(
                   connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
                   tableName: configuration.GetSection("Serilog:TableName").Value,
                   appConfiguration: configuration,
autoCreateSqlTable: true,
                   columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
                   schemaName: configuration.GetSection("Serilog:SchemaName").Value)
               .CreateLogger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
