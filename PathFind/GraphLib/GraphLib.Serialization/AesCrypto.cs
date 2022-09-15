using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace GraphLib.Serialization
{
    internal sealed class AesCrypto : ICrypto
    {
        private const int KeyLength = 32;
        private const int IVLength = 16;
        private const string CryptoString = "8~SBmlph7sfHLi?Od}CPrU5k{uNMAnYT";

        private readonly Lazy<byte[]> key;
        private readonly Lazy<byte[]> iv;

        public byte[] Key => key.Value;

        public byte[] IV => iv.Value;

        public AesCrypto()
        {
            key = new Lazy<byte[]>(() => CreateCryptoStringBytes(KeyLength));
            iv = new Lazy<byte[]>(() => CreateCryptoStringBytes(IVLength));
        }

        private byte[] CreateCryptoStringBytes(int length)
        {
            var chunk = new string(CryptoString.Take(length).ToArray());
            return Encoding.ASCII.GetBytes(chunk);
        }
    }
}
