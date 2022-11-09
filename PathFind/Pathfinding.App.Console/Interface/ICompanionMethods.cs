using System;
using System.Reflection;

namespace Pathfinding.App.Console.Interface
{
    internal interface ICompanionMethods<out TResult>
        where TResult : Delegate
    {
        TResult GetMethods(MethodInfo targetMethod);
    }
}
