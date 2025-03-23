using Base.Api.Filters;

namespace Base.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelFilter>();
                options.Filters.Add<ApiExceptionFilter>();
            });

            return services;
        }
    }
}
