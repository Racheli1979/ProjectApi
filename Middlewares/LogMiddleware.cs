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
        logger.LogInformation($"Hundling requst: {c.Request.Path}.{c.Request.Method}");
        await next(c);
        logger.LogInformation($"Finished hundling requst: {c.Request.Path}.{c.Request.Method} Response status: {c.Response.StatusCode}");
    }
}

public static partial class MiddlewareExtensionsF
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogMiddleware>();
    }
}