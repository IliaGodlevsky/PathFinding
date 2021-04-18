using System;
using AssembleClassesLib.EventHandlers;

namespace AssembleClassesLib.Interface
{
    public interface IAssembleClasses : IDisposable
    {
        event AssembleClassesEventHandler OnClassesLoaded;

        string[] ClassesNames { get; }

        object Get(string name, 
            params object[] ctorParametres);

        void LoadClasses();
    }
}