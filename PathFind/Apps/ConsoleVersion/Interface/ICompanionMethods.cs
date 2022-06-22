using System.Reflection;

namespace ConsoleVersion.Interface
{
    internal interface ICompanionMethods<out TResult>
    {
        TResult GetMethods(MethodInfo targetMethod);
    }
}
