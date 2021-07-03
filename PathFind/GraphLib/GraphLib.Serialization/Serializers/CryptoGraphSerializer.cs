using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GraphLib.Serialization.Serializers
{
    public sealed class CryptoGraphSerializer : IGraphSerializer
    {
        public CryptoGraphSerializer(IGraphSerializer serializer,
            SymmetricAlgorithm algorithm)
        {
            this.serializer = serializer;
            this.algorithm = algorithm;
            key = new Lazy<byte[]>(() => GetCryptoStringBytes(algorithm.Key.Length));
            IV = new Lazy<byte[]>(() => GetCryptoStringBytes(algorithm.IV.Length));
        }

        public CryptoGraphSerializer(IGraphSerializer serializer)
            : this(serializer, new AesCryptoServiceProvider())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="CantSerializeGraphException"></exception>
        public IGraph LoadGraph(Stream stream)
        {
            IGraph graph = null;
            try
            {
                algorithm.Padding = PaddingMode.None;
                using (var decryptor = algorithm.CreateDecryptor(key.Value, IV.Value))
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
        /// Saves graph in stream
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        /// <exception cref="CantSerializeGraphException"></exception>
        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                algorithm.Padding = PaddingMode.PKCS7;
                using (var encryptor = algorithm.CreateEncryptor(key.Value, IV.Value))
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

        private byte[] GetCryptoStringBytes(int length)
        {
            var cryptoStringChunk = CryptoString.Take(length).AsString();
            return Encoding.ASCII.GetBytes(cryptoStringChunk);
        }

        // 256 bytes
        private const string CryptoString = 
            "8~SBmlph7sfHLi?O" +
            "d}CPrU5k{uNMAnYT" +
            "#6EWzbjQta1|{ass" +
            "qs5Gzv}XE{D*FRh@" +
            "LsCeYzIHxqcbu*i?" +
            "gPv3m9$AStFpWw8K" +
            "0Go7n61@VO6QoR0N" +
            "CshkUyfoZZ}W|$a2" +
            "fCXgDYj6vLye{~06" +
            "On3Zg5bBNvy9@DBf" +
            "8VCLgtZE7zdAfRNE" +
            "vz}lqSpTaMfYEagV" +
            "3rJaYMygE?zCvfOV" +
            "9@rfgcX#Lpb6PsZh" +
            "$dsBuoR|LSYb~Z?z" +
            "0JrLM73m%nCgQ1sL";

        private readonly Lazy<byte[]> key;
        private readonly Lazy<byte[]> IV;
        private readonly IGraphSerializer serializer;
        private readonly SymmetricAlgorithm algorithm;
    }
}
