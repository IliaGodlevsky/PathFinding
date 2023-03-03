using Shared.Primitives.Single;

namespace Shared.Random.Realizations
{
    public sealed class NullRandom : Singleton<NullRandom, IRandom>, IRandom
    {
        private NullRandom()
        {

        }

        public uint NextUInt()
        {
            return default;
        }
    }
}
