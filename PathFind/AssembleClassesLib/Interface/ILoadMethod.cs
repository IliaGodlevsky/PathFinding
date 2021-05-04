using System.Reflection;

namespace AssembleClassesLib.Interface
{
    public interface ILoadMethod
    {
        Assembly Load(string assemblyPath);
    }
}
