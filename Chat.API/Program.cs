using Chat.API.Entities;
using Chat.API.Functions.User;
using Chat.API.SignalR;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//db settings
builder.Services.AddDbContext<ChatAppContext> (context =>
    context.UseMySql(ChatAppContext.connectionString, ServerVersion.AutoDetect(ChatAppContext.connectionString)));

builder.Services.AddTransient(connection => new MySqlConnection(ChatAppContext.connectionString));


//user settings
builder.Services.AddTransient<IUserManager, UserManager>();

//signalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//signalR
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/ChatHub");
});

app.Run();
