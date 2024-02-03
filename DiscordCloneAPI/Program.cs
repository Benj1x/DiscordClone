using DiscordCloneAPI.DBContexts;
using DiscordCloneAPI.Utilities;
using DiscordCloneAPI.Utilities.Functions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ServerContext>(opt =>
    opt.UseInMemoryDatabase("ServerList"));

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("UserList"));

builder.Services.AddDbContext<MessageContext>(opt =>
    opt.UseInMemoryDatabase("MessageList"));

builder.Services.AddDbContext<MembershipContext>(opt =>
    opt.UseInMemoryDatabase("MembershipList"));

builder.Services.AddTransient<UServerMembership>();

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
