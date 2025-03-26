using System.Diagnostics;

namespace ProjectApi.Middlewares;

public class LogMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }
    public async Task Invoke(HttpContext c)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next.Invoke(c);
        logger.LogDebug($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}");
        
    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogMiddleware>();
    }
}