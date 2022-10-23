namespace TeamsNPlayers.WebApi.Middlewares;

public class RelativeLocationMiddleware
{
    public static async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.OnStarting(OnStarting, context);
        
        await next(context);
    }

    private static Task OnStarting(object state)
    {
        if (state is HttpContext { Response.Headers: { Location: { Count: > 0 } location} headers })
        {
            headers.Location = new Uri(location).PathAndQuery;
        }
        
        return Task.CompletedTask;
    }
}