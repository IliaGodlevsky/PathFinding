namespace Shared.Random.Realizations
{
    public sealed class DummyRandom : IRandom
    {
        private uint next = 0;

        public uint NextUInt()
        {
            return next++;
        }
    }
}
