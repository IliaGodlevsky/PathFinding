using System;

namespace AssembleClassesLib.EventArguments
{
    public class AssembleClassesEventArgs : EventArgs
    {
        public string[] LoadedPluginsKeys { get; }

        public AssembleClassesEventArgs(string[] loadedPluginsNames)
        {
            LoadedPluginsKeys = loadedPluginsNames;
        }
    }
}