using Random.Interface;
using System;
using System.Security.Cryptography;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Realizations
{
    /// <summary>
    /// A random number generator 
    /// base on <see cref="RNGCryptoServiceProvider"/>
    /// </summary>
    public sealed class InclusiveRangeRandom : IRandom, IDisposable
    {
        private const int IntSize = sizeof(int);
        private const int MaxBufferSize = IntSize << 4;

        public InclusiveRangeRandom()
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
                uint number = GetUInt32();
                currentBufferPosition += IntSize;
                VerifyBuffer();
                long amplitude = (long)range.Amplitude() + 1;
                return amplitude == 0 ? range.LowerValueOfRange
                    : (int)(number % amplitude)
                    + range.LowerValueOfRange;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~InclusiveRangeRandom()
        {
            Dispose(false);
        }

        private uint GetUInt32()
        {
            return (uint)Math.Abs(BitConverter.ToInt32(buffer, currentBufferPosition));
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

        private readonly object lockObject;
        private bool isDisposing;
        private readonly byte[] buffer;
        private readonly RNGCryptoServiceProvider provider;

        private int currentBufferPosition;
    }
}