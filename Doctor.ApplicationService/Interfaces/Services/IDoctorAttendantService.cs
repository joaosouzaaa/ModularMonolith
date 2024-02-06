using Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.ApplicationService.Interfaces.Services;
public interface IDoctorAttendantService
{
    Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave);
    Task<bool> UpdateAsync(DoctorAttendantUpdate doctorAttendantUpdate);
    Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterRequest filterRequest);
    Task<DoctorAttendantResponse?> GetByIdAsync(int id);
}
