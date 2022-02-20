using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullExecutable : Singleton<NullExecutable, IExecutable<IVertex>>, IExecutable<IVertex>
    {
        public void Execute(IVertex vertex)
        {

        }

        private NullExecutable()
        {

        }
    }
}
