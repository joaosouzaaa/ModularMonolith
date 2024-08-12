using Doctor.Domain.Arguments;
using Doctor.Domain.Entities;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.Interfaces.Repositories;

public interface IDoctorAttendantRepository
{
    Task<bool> AddAsync(DoctorAttendant doctorAttendant, CancellationToken cancellationToken);
    Task<PageList<DoctorAttendant>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterArgument filter, CancellationToken cancellationToken);
    Task<DoctorAttendant?> GetByIdAsync(int id, bool asNoTracking, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DoctorAttendant doctorAttendant, CancellationToken cancellationToken);
}
