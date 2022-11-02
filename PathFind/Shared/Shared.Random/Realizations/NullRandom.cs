using NullObject.Attributes;
using Shared.Random.Interface;
using SingletonLib;
using System.Diagnostics;

namespace Shared.Random.Realizations
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