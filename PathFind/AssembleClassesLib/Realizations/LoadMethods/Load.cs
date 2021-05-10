using AssembleClassesLib.Interface;
using System.Reflection;

namespace AssembleClassesLib.Realizations.LoadMethods
{
    public sealed class Load : IAssembleLoadMethod
    {
        Assembly IAssembleLoadMethod.Load(string assemblyPath)
        {
            return Assembly.Load(assemblyPath);
        }
    }
}
