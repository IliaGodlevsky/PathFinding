using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleVersion.Interface
{
    internal interface IDelegateExtractor<TDelegate>
        where TDelegate : Delegate
    {
        IEnumerable<TDelegate> Extract(MethodInfo info, object target);
    }
}
