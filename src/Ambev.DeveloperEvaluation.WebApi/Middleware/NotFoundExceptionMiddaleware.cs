using System.Net;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class NotFoundExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<NotFoundExceptionMiddleware> _logger;

        public NotFoundExceptionMiddleware(RequestDelegate next, ILogger<NotFoundExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Key not found exception caught");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = ex.Message,
                    errors = new List<string> { ex.Message }
                });
            }
        }
    }
}