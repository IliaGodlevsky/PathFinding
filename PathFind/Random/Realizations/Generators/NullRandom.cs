using NullObject.Attributes;
using Random.Interface;
using SingletonLib;
using System.Diagnostics;

namespace Random.Realizations.Generators
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullRandom : Singleton<NullRandom, IRandom>, IRandom
    {
        public uint NextUint()
        {
            return default;
        }

        private NullRandom()
        {

        }
    }
}