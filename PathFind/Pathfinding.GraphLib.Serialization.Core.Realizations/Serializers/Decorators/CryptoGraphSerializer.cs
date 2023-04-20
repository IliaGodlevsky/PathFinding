using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Serializers.Decorators
{
    public sealed class CryptoGraphSerializer<TGraph, TVertex> : IGraphSerializer<TGraph, TVertex>, IDisposable
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly ICrypto crypto;
        private readonly IGraphSerializer<TGraph, TVertex> serializer;
        private readonly SymmetricAlgorithm algorithm;

        public CryptoGraphSerializer(
            IGraphSerializer<TGraph, TVertex> serializer, 
            SymmetricAlgorithm algorithm, 
            ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        public CryptoGraphSerializer(
            IGraphSerializer<TGraph, TVertex> serializer,
            SymmetricAlgorithm algorithm)
            : this(serializer, algorithm, new AesCrypto())
        {
        }

        public CryptoGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
            : this(serializer, Aes.Create(), new AesCrypto())
        {
        }

        public TGraph DeserializeFrom(Stream stream)
        {
            try
            {
                using (var decryptor = algorithm.CreateDecryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read, leaveOpen: true))
                    {
                        return serializer.DeserializeFrom(cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(TGraph graph, Stream stream)
        {
            try
            {
                using (var encryptor = algorithm.CreateEncryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write, leaveOpen: true))
                    {
                        serializer.SerializeTo(graph, cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            algorithm.Dispose();
        }
    }
}