namespace Base.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMiddleware<RequestLoggingMiddleware>()
                .UseMiddleware<CorrelationIdMiddleware>()
                .UseMiddleware<RateLimitMiddleware>()
                .UseMiddleware<AuthenticationMiddleware>();
        }
    }

}
