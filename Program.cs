using ItemApi.Data;
using ItemApi.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ��������� �������� MongoDB �� ����� ������������
var mongoDBSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();

// ����������� �������� MongoDB � DI-����������
builder.Services.AddSingleton(mongoDBSettings);

// ����������� ������� MongoDB
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(mongoDBSettings.ConnectionString));

// ����������� ������� ��� ������ � �������
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
