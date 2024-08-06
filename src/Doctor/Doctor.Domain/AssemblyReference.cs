using System.Reflection;

namespace Doctor.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
