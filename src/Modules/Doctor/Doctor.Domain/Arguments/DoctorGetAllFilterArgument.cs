using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.Arguments;

public sealed class DoctorGetAllFilterArgument : PageParameters
{
    public List<int> SpecialityIds { get; set; } = [];
    public DateTime? InitialTime { get; set; }
    public DateTime? FinalTime { get; set; }
}
