using System;
using System.Security.Cryptography;

using static System.BitConverter;

namespace Shared.Random.Realizations
{
    /// <summary>
    /// A random number generator that produces a 
    /// cryptographically strong random sequence of numbers
    /// </summary>
    public sealed class CryptoRandom : IRandom, IDisposable
    {
        private const int Increment = 4;

        private readonly int maxBufferSize = 2048;
        private readonly byte[] buffer;
        private readonly RandomNumberGenerator rng;

        private int currentPosition = -Increment;

        public CryptoRandom()
        {
            rng = RandomNumberGenerator.Create();
            buffer = new byte[maxBufferSize];
            rng.GetBytes(buffer);
        }

        public uint NextUInt()
        {
            currentPosition += Increment;
            if (currentPosition >= maxBufferSize)
            {
                currentPosition = 0;
                rng.GetBytes(buffer);
            }
            return ToUInt32(buffer, currentPosition);
        }

        public void Dispose()
        {
            rng.Dispose();
        }
    }
}