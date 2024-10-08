﻿using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.DataTransferObjects.DoctorAttendant;

public sealed class DoctorGetAllFilterRequest : PageParameters
{
    public List<int> SpecialityIds { get; set; } = [];
    public DateTime? InitialTime { get; set; }
    public DateTime? FinalTime { get; set; }
}
