using ItemApi.Data;
using ItemApi.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Получение настроек MongoDB из файла конфигурации
var mongoDBSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();

// Регистрация настроек MongoDB в DI-контейнере
builder.Services.AddSingleton(mongoDBSettings);

// Регистрация клиента MongoDB
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(mongoDBSettings.ConnectionString));

// Регистрация сервиса для работы с данными
builder.Services.AddSingleton<ItemService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
