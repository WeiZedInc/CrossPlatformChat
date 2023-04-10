using Chat.API;
using Chat.API.Managers;
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


builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<IChatManager, ChatManager>();

//signalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHttpsRedirection(); // turn on https on release



app.MapControllers();

//signalR
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatHub>("/ChatHub");

app.Run();
