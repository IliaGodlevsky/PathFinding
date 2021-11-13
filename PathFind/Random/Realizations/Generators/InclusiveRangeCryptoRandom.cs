using Random.Interface;
using System;
using System.Security.Cryptography;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Realizations
{
    /// <summary>
    /// A random number generator 
    /// based on <see cref="RNGCryptoServiceProvider"/>
    /// </summary>
    public sealed class InclusiveRangeCryptoRandom : IRandom, IDisposable
    {
        private const int IntSize = sizeof(int);
        private const int MaxBufferSize = IntSize << 4;

        private uint Seed
        {
            get
            {
                uint number = BitConverter.ToUInt32(buffer, currentBufferPosition);
                currentBufferPosition += IntSize;
                VerifyBuffer();
                return number;
            }
        }
        

        public InclusiveRangeCryptoRandom()
        {
            provider = new RNGCryptoServiceProvider();
            buffer = new byte[MaxBufferSize];
            currentBufferPosition = 0;
            provider.GetBytes(buffer);
            lockObject = new object();
        }

        public int Next(int minValue, int maxValue)
        {
            lock (lockObject)
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
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing == false || isDisposing)
            {
                return;
            }

            isDisposing = true;
            provider.Dispose();
        }

        private void VerifyBuffer()
        {
            if (currentBufferPosition >= MaxBufferSize)
            {
                currentBufferPosition = 0;
                provider.GetBytes(buffer);
            }
        }

        ~InclusiveRangeCryptoRandom()
        {
            Dispose(false);
        }

        private readonly object lockObject;
        private readonly byte[] buffer;
        private readonly RNGCryptoServiceProvider provider;

        private bool isDisposing;
        private int currentBufferPosition;
    }
}