﻿using Doctor.ApplicationService.DataTransferObjects.Certification;

namespace Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
public sealed record DoctorAttendantSave(string Name,
                                         int ExperienceYears,
                                         DateTime BirthDate,
                                         CertificationRequest Certification,
                                         List<int> SpecialityIds);
