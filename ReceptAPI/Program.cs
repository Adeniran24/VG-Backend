using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using ReceptAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReceptdbContext>(options =>
    options.UseMySql(
        "SERVER=localhost;PORT=3306;DATABASE=receptdb;USER=root;PASSWORD=;",
        MySqlServerVersion.AutoDetect("SERVER=localhost;PORT=3306;DATABASE=receptdb;USER=root;PASSWORD=")
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.Run();
