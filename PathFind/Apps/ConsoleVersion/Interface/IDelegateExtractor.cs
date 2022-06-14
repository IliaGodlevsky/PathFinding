using System;
using System.Reflection;

namespace ConsoleVersion.Interface
{
    internal interface IDelegateExtractor<TDelegate, TResult>
        where TDelegate : Delegate
    {
        TResult Extract(MethodInfo info, object target);
    }
}
