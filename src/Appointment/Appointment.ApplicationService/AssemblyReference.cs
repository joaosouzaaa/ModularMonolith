using System.Reflection;

namespace Appointment.ApplicationService;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
