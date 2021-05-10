using System.Reflection;

namespace AssembleClassesLib.Interface
{
    public interface IAssembleLoadMethod
    {
        Assembly Load(string assemblyPath);
    }
}
