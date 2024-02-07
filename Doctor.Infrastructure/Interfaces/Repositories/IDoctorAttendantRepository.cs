using Doctor.Domain.Arguments;
using Doctor.Domain.Entities;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Infrasctructure.Interfaces.Repositories;
public interface IDoctorAttendantRepository
{
    Task<bool> AddAsync(DoctorAttendant doctorAttendant);
    Task<bool> UpdateAsync(DoctorAttendant doctorAttendant);
    Task<PageList<DoctorAttendant>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterArgument filter);
    Task<DoctorAttendant?> GetByIdAsync(int id, bool asNoTracking);
}
