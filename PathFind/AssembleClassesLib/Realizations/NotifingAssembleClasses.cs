using System.Collections.Generic;
using AssembleClassesLib.EventArguments;
using AssembleClassesLib.EventHandlers;
using AssembleClassesLib.Interface;

namespace AssembleClassesLib.Realizations
{
    public sealed class NotifingAssembleClasses : INotifingAssembleClasses
    {
        public event AssembleClassesEventHandler OnClassesLoaded;

        public NotifingAssembleClasses(IAssembleClasses assembleClasses)
        {
            this.assembleClasses = assembleClasses;
            ClassesNames = assembleClasses.ClassesNames;
        }

        public IReadOnlyCollection<string> ClassesNames { get; private set; }

        public object Get(string name, params object[] ctorParametres)
        {
            return assembleClasses.Get(name, ctorParametres);
        }

        public void LoadClasses()
        {
            assembleClasses.LoadClasses();
            ClassesNames = assembleClasses.ClassesNames;
            var args = new AssembleClassesEventArgs(ClassesNames);
            OnClassesLoaded?.Invoke(this, args);
        }

        private readonly IAssembleClasses assembleClasses;
    }
}