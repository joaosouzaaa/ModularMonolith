using Microsoft.Extensions.DependencyInjection;
using Patient.ApplicationServices.Mappers;
using Patient.Domain.Interfaces.Mappers;

namespace Patient.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IContactInfoMapper, ContactInfoMapper>();
        services.AddScoped<IPatientClientMapper, PatientClientMapper>();
    }
}
