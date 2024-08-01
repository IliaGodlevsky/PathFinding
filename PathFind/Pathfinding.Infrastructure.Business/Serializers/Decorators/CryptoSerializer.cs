using Pathfinding.Infrastructure.Business.Serializers.Exceptions;
using Pathfinding.Service.Interface;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Serializers.Decorators
{
    public sealed class CryptoSerializer<T> : ISerializer<T>, IDisposable
    {
        private readonly ICrypto crypto;
        private readonly ISerializer<T> serializer;
        private readonly SymmetricAlgorithm algorithm;

        public CryptoSerializer(
            ISerializer<T> serializer,
            SymmetricAlgorithm algorithm,
            ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        public CryptoSerializer(
            ISerializer<T> serializer,
            SymmetricAlgorithm algorithm)
            : this(serializer, algorithm, new AesCrypto())
        {
        }

        public CryptoSerializer(ISerializer<T> serializer)
            : this(serializer, Aes.Create(), new AesCrypto())
        {
        }

        public async Task<T> DeserializeFromAsync(Stream stream, CancellationToken token = default)
        {
            try
            {
                using (var decryptor = algorithm.CreateDecryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read, leaveOpen: true))
                    {
                        return await serializer.DeserializeFromAsync(cryptoStream, token);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public async Task SerializeToAsync(T graph, Stream stream, CancellationToken token = default)
        {
            try
            {
                using (var encryptor = algorithm.CreateEncryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write, leaveOpen: true))
                    {
                        await serializer.SerializeToAsync(graph, cryptoStream, token);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            algorithm.Dispose();
        }
    }
}