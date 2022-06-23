using System;
using System.Reflection;

namespace ConsoleVersion.Interface
{
    internal interface ICompanionMethods<out TResult>
        where TResult : Delegate
    {
        TResult GetMethods(MethodInfo targetMethod);
    }
}
