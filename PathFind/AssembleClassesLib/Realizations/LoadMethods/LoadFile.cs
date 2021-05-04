using AssembleClassesLib.Interface;
using System.Reflection;

namespace AssembleClassesLib.Realizations.LoadMethods
{
    public sealed class LoadFile : ILoadMethod
    {
        public Assembly Load(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
