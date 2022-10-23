using TeamsNPlayers.Application;
using TeamsNPlayers.Infrastructure.EFCore;
using TeamsNPlayers.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ApplicationRegistration.RegisterServices(builder.Services);
InfrastructureEfCoreRegistration.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await InfrastructureEfCoreRegistration.Configure(app.Services);

app.UseAuthorization();
app.MapControllers();

app.Use(RelativeLocationMiddleware.InvokeAsync);

await app.RunAsync();