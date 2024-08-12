using System.Reflection;

namespace Doctor.ApplicationService;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
