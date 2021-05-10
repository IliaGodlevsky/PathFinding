using AssembleClassesLib.Interface;
using System.Reflection;

namespace AssembleClassesLib.Realizations.LoadMethods
{
    public sealed class LoadFrom : IAssembleLoadMethod
    {
        public Assembly Load(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
