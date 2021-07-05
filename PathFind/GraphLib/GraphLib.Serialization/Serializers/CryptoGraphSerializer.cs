using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Serializers
{
    public sealed class CryptoGraphSerializer : IGraphSerializer
    {
        public CryptoGraphSerializer(
            IGraphSerializer serializer,
            SymmetricAlgorithm algorithm,
            ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        public CryptoGraphSerializer(IGraphSerializer serializer)
            : this(serializer,
                  new AesCryptoServiceProvider(),
                  new AesCrypto())
        {

        }

        public IGraph LoadGraph(Stream stream)
        {
            IGraph graph = null;
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

        /// <summary>
        /// Saves graph in stream and encrypts it
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
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

        private readonly ICrypto crypto;
        private readonly IGraphSerializer serializer;
        private readonly SymmetricAlgorithm algorithm;
    }
}