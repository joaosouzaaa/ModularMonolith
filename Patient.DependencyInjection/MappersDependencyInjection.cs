using Microsoft.Extensions.DependencyInjection;
using Patient.ApplicationServices.Intefaces.Mappers;
using Patient.ApplicationServices.Mappers;

namespace Patient.DependencyInjection;
public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IContactInfoMapper, ContactInfoMapper>();
        services.AddScoped<IPatientClientMapper, PatientClientMapper>();
    }
}
