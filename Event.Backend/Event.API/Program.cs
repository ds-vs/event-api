using Event.DAL;
using Event.DAL.Repositories;
using Event.Domain.Repositories.Interfaces;
using Event.Service;
using Event.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Pgsql");

builder.Services.AddDbContext<EventDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IEventService, EventService>();

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
