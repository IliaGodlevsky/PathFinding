using AssembleClassesLib.Interface;
using Common.Attributes;
using System.Collections.Generic;

namespace AssembleClassesLib.Realizations.AssembleClassesImpl
{
    [Null]
    public sealed class NullAssembleClasses : IAssembleClasses
    {
        public NullAssembleClasses()
        {
            ClassesNames = new string[] { };
            nullObject = new object();
        }

        public IReadOnlyCollection<string> ClassesNames { get; }

        public object Get(string name, params object[] ctorParametres)
        {
            return nullObject;
        }

        public void LoadClasses()
        {

        }

        private readonly object nullObject;
    }
}
