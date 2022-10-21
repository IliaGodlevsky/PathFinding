using Random.Interface;
using System;
using System.Security.Cryptography;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Realizations.Generators
{
    public sealed class CryptoRandom : IRandom, IDisposable
    {
        private const int IntSize = sizeof(int);
        private const int MaxBufferSize = IntSize << 4;

        private readonly byte[] buffer;
        private readonly RandomNumberGenerator generator;

        private bool isDisposing;
        private int currentBufferPosition;

        private uint Seed
        {
            get
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
        }

        public CryptoRandom()
        {
            generator = RandomNumberGenerator.Create();
            buffer = new byte[MaxBufferSize];
            currentBufferPosition = 0;
            generator.GetBytes(buffer);
        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            long module = (long)range.Amplitude() + 1;
            long max = 1 + (long)uint.MaxValue;
            long remainder = max % module;
            uint seed = Seed;
            while (seed >= max - remainder)
            {
                seed = Seed;
            }
            return (int)(seed % module) + range.LowerValueOfRange;
        }

        public void Dispose()
        {
            generator.Dispose();
        }
    }
}