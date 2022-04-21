using System;
using System.Collections.Generic;

namespace ConsoleVersion.Interface
{
    internal interface IDelegateExtractor<TDelegate>
        where TDelegate : Delegate
    {
        IEnumerable<TDelegate> Create(object target);
    }
}
