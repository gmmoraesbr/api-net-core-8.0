using Base.Application.Mappings;

namespace Base.Api.Extensions;

public static class MappingExtensions
{
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMappingProfile));
        services.AddAutoMapper(typeof(ProductMapperProfile));
        return services;
    }
}