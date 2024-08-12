using ModularMonolith.Common.Settings.NotificationSettings;
using System.Net.Mime;
using System.Text.Json;

namespace ModularMonolith.API.Middlewares;

public sealed class UnexpectedErrorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = new List<Notification>()
            {
                new("Unexpected Error",
                    "An unexpected error happened.")
            };

            var responseJsonString = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(responseJsonString);
        }
    }
}
