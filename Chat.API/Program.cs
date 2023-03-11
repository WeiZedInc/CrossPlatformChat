
using Chat.API.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppContext>(option =>
{
    option.UseMySql(ChatAppContext.connectionString, ServerVersion.AutoDetect(ChatAppContext.connectionString));
});

builder.Services.AddTransient<MySqlConnection>(_ =>
new MySqlConnection(ChatAppContext.connectionString));

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
