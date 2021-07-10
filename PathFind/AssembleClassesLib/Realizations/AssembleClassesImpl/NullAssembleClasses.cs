using AssembleClassesLib.Interface;
using NullObject.Attributes;
using System.Collections.Generic;

namespace AssembleClassesLib.Realizations.AssembleClassesImpl
{
    [Null]
    public sealed class NullAssembleClasses : IAssembleClasses
    {
        public NullAssembleClasses()
        {
            ClassesNames = new string[] { };
            instance = new object();
        }

        public IReadOnlyCollection<string> ClassesNames { get; }

        public object Get(string name, params object[] ctorParametres)
        {
            return instance;
        }

        public void LoadClasses()
        {

        }

        private readonly object instance;
    }
}
