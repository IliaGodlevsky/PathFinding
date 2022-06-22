using System;
using System.Reflection;

namespace ConsoleVersion.Interface
{
    internal interface ICompanionMethods<TResult>
    {
        TResult GetMethods(MethodInfo targetMethod);
    }
}
