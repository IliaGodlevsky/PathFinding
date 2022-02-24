using Commands.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Diagnostics;

namespace Commands.Realizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullExecutable<T> : Singleton<NullExecutable<T>, IExecutable<T>>, IExecutable<T>
    {
        public void Execute(T obj)
        {

        }

        private NullExecutable()
        {

        }
    }
}
