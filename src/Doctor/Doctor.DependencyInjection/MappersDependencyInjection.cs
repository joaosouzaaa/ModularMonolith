using Doctor.ApplicationService.Mappers;
using Doctor.Domain.Interfaces.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICertificationMapper, CertificationMapper>();
        services.AddScoped<IDoctorAttendantMapper, DoctorAttendantMapper>();
        services.AddScoped<IScheduleMapper, ScheduleMapper>();
        services.AddScoped<ISpecialityMapper, SpecialityMapper>();
    }
}
