using System;
using System.Security.Cryptography;

namespace Shared.Random.Realizations
{
    /// <summary>
    /// A random number generator that produces a 
    /// cryptographically strong random sequence of numbers
    /// </summary>
    public sealed class CryptoRandom : IRandom, IDisposable
    {
        private const int PositionIncrement = 4;
        private const int MaxBufferSize = 512;

        private readonly byte[] buffer;
        private readonly RandomNumberGenerator generator;

        private int currentBufferPosition;

        public CryptoRandom()
        {
            generator = RandomNumberGenerator.Create();
            buffer = new byte[MaxBufferSize];
            currentBufferPosition = 0;
            generator.GetBytes(buffer);
        }

        public uint NextUInt()
        {
            uint number = BitConverter.ToUInt32(buffer, currentBufferPosition);
            currentBufferPosition += PositionIncrement;
            if (currentBufferPosition >= MaxBufferSize)
            {
                currentBufferPosition = 0;
                generator.GetBytes(buffer);
            }
            return number;
        }

        public void Dispose()
        {
            generator.Dispose();
        }
    }
}