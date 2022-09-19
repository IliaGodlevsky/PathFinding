using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Serializers.Decorators
{
    public sealed class CryptoGraphSerializer : IGraphSerializer, IDisposable
    {
        private readonly ICrypto crypto;
        private readonly IGraphSerializer serializer;
        private readonly SymmetricAlgorithm algorithm;

        public CryptoGraphSerializer(IGraphSerializer serializer, SymmetricAlgorithm algorithm, ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        public CryptoGraphSerializer(IGraphSerializer serializer)
            : this(serializer, new AesCryptoServiceProvider(), new AesCrypto())
        {

        }

        public IGraph LoadGraph(Stream stream)
        {
            var graph = NullGraph.Interface;
            try
            {
                algorithm.Padding = PaddingMode.None;
                using (var decryptor = algorithm.CreateDecryptor(crypto.Key, crypto.IV))
                {
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read, leaveOpen: true))
                    {
                        graph = serializer.LoadGraph(cryptoStream);
                    }
                }
                return graph;
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
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
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            algorithm.Dispose();
        }
    }
}