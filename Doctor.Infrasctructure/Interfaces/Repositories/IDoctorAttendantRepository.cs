using Doctor.Domain.Entities;

namespace Doctor.Infrasctructure.Interfaces.Repositories;
public interface IDoctorAttendantRepository
{
    Task<bool> AddAsync(DoctorAttendant doctorAttendant);
    Task<bool> UpdateAsync(DoctorAttendant doctorAttendant);
}
