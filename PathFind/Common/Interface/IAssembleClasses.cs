using System;
using Common.EventHandlers;

namespace Common.Interface
{
    public interface IAssembleClasses<out T> : IDisposable where T : class
    {
        event AssembleClassesEventHandler OnClassesLoaded;

        string[] ClassesNames { get; }

        T Get(string name, params object[] ctorParametres);

        void LoadClasses();
    }
}