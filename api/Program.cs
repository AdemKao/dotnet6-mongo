using api.Models;
using api.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services
    .AddSingleton<IMongoDbSettings, MongoDbSettings>(
        sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value)
    .AddSingleton<IMongoClient>(
        s => new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

builder.Services.AddScoped<IStudentService, StudentService>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
