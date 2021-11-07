using Common.Extensions;
using Common.ValueRanges;
using System;
using System.Security.Cryptography;

namespace Common
{
    public class CryptoRandom : Random, IDisposable
    {
        private const int BufferSize = 4;
        private const int MaxBufferSize = 512;
        private const int IntSize = sizeof(int);

        public CryptoRandom()
        {
            provider = new RNGCryptoServiceProvider();
            buffer = new byte[MaxBufferSize];
            currentBufferPosition = 0;
            provider.GetBytes(buffer);
        }

        public override int Next()
        {
            return Next(int.MinValue, int.MaxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            int number = Math.Abs(BitConverter.ToInt32(buffer, currentBufferPosition));
            currentBufferPosition += IntSize;
            VerifyBuffer();
            uint amplitude = (uint)(maxValue - minValue);
            if (amplitude == 0)
            {
                return default;
            }
            return (int)(number % amplitude) + minValue;
        }

        public override int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            provider.GetBytes(buffer);
        }

        public override double NextDouble()
        {
            return Next(0, int.MaxValue) / int.MaxValue;
        }

        public void Dispose()
        {
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

        private readonly byte[] buffer;
        private readonly RNGCryptoServiceProvider provider;
        private int currentBufferPosition;
    }
}