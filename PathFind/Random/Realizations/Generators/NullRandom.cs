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
        public int Next(int minValue, int maxValue)
        {
            return 0;
        }

        private NullRandom()
        {

        }
    }
}