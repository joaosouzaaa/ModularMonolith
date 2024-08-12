using System.Reflection;

namespace Patient.ApplicationServices;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
