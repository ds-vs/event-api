using Event.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddSwaggerConfiguration();

    services.AddRepository();

    services.AddService();

    services.AddAuthenticationConfiguration(builder.Configuration);

    services.AddDatabase(builder.Configuration);

    services.AddControllers();

    services.AddEndpointsApiExplorer();
}
