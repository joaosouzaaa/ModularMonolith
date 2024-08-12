using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.Interfaces.Services;

public interface IDoctorAttendantService
{
    Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave, CancellationToken cancellationToken);
    Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterRequest filterRequest, CancellationToken cancellationToken);
    Task<DoctorAttendantResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DoctorAttendantUpdate doctorAttendantUpdate, CancellationToken cancellationToken);
}
