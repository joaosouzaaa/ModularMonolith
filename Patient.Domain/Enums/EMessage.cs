using System.ComponentModel;

namespace Patient.Domain.Enums;
public enum EMessage : ushort
{
    [Description("{0} has invalid length. It should be {1}.")]
    InvalidLength,

    [Description("{0} was not found.")]
    NotFound,

    [Description("{0} has invalid format.")]
    InvalidFormat
}
