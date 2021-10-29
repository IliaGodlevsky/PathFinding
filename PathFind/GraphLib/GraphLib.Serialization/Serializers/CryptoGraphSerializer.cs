using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Serializers
{
    /// <summary>
    /// A cryptographic graph serializer
    /// </summary>
    public sealed class CryptoGraphSerializer : IGraphSerializer
    {
        /// <summary>
        /// Creates new <see cref="CryptoGraphSerializer"/>
        /// using some <see cref="SymmetricAlgorithm"/> and
        /// <see cref="ICrypto"/>
        /// </summary>
        /// <param name="serializer"><see cref="IGraphSerializer"/> which 
        /// will serialize a crypto stream, created by <see cref="CryptoGraphSerializer"/></param>
        /// <param name="algorithm">Algorithm to create ecnryptor and decryptor</param>
        /// <param name="crypto">A constant key and initializing vector provider</param>
        public CryptoGraphSerializer(
            IGraphSerializer serializer,
            SymmetricAlgorithm algorithm,
            ICrypto crypto)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            this.crypto = crypto;
        }

        /// <summary>
        /// Creates new <see cref="CryptoGraphSerializer"/>, 
        /// using <see cref="AesCryptoServiceProvider"/> and
        /// <see cref="AesCrypto"/>
        /// </summary>
        /// <param name="serializer"></param>
        public CryptoGraphSerializer(IGraphSerializer serializer)
            : this(serializer,
                  new AesCryptoServiceProvider(),
                  new AesCrypto())
        {

        }

        /// <summary>
        /// Loads graph from stream, that must be encrypted later
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public IGraph LoadGraph(Stream stream)
        {
            IGraph graph = NullGraph.Instance;
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

        private readonly ICrypto crypto;
        private readonly IGraphSerializer serializer;
        private readonly SymmetricAlgorithm algorithm;
    }
}