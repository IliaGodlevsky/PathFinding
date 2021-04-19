using System;
using System.Collections.Generic;

namespace AssembleClassesLib.EventArguments
{
    public sealed class AssembleClassesEventArgs : EventArgs
    {
        public IEnumerable<string> ClassesNames { get; }

        public AssembleClassesEventArgs(IEnumerable<string> loadedPluginsNames)
        {
            ClassesNames = loadedPluginsNames;
        }
    }
}