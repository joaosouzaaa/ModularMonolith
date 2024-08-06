using System.Reflection;

namespace Patient.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
