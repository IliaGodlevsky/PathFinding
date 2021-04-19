using System.Collections.Generic;

namespace AssembleClassesLib.Interface
{
    public interface IAssembleClasses
    {
        IReadOnlyCollection<string> ClassesNames { get; }

        object Get(string name, params object[] ctorParametres);

        void LoadClasses();
    }
}