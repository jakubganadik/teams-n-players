using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TeamsNPlayers.Application;

public static class ApplicationRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}