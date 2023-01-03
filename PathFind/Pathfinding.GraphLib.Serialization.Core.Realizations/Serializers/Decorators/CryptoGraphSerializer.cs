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

        public CryptoGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer, SymmetricAlgorithm algorithm, ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        public CryptoGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
            : this(serializer, new AesCryptoServiceProvider(), new AesCrypto())
        {

        }

        public TGraph LoadGraph(Stream stream)
        {
            try
            {
                algorithm.Padding = PaddingMode.None;
                using (var decryptor = algorithm.CreateDecryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read, leaveOpen: true))
                    {
                        return serializer.LoadGraph(cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph<IVertex> graph, Stream stream)
        {
            try
            {
                algorithm.Padding = PaddingMode.PKCS7;
                using (var encryptor = algorithm.CreateEncryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write, leaveOpen: true))
                    {
                        serializer.SaveGraph(graph, cryptoStream);
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