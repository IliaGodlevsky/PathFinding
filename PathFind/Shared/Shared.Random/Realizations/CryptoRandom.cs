using Shared.Random.Interface;
using System;
using System.Security.Cryptography;

namespace Shared.Random.Realizations.Generators
{
    public sealed class CryptoRandom : IRandom, IDisposable
    {
        private const int IntSize = sizeof(int);
        private const int MaxBufferSize = IntSize << 4;

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

        public uint NextUint()
        {
            uint number = BitConverter.ToUInt32(buffer, currentBufferPosition);
            currentBufferPosition += IntSize;
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