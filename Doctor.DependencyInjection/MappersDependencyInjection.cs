using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICertificationMapper, CertificationMapper>();
        services.AddScoped<IDoctorAttendantMapper, DoctorAttendantMapper>();
        services.AddScoped<IScheduleMapper, ScheduleMapper>();
        services.AddScoped<ISpecialityMapper, SpecialityMapper>();
    }
}
