using System.ComponentModel;

namespace Appointment.Domain.Enums;
public enum EMessage : ushort
{
    [Description("{0} has to be greater than {1}.")]
    GreaterThan
}
