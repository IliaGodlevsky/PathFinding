using AssembleClassesLib.Interface;
using System.Reflection;

namespace AssembleClassesLib.Realizations.LoadMethods
{
    public sealed class Load : ILoadMethod
    {
        Assembly ILoadMethod.Load(string assemblyPath)
        {
            return Assembly.Load(assemblyPath);
        }
    }
}
