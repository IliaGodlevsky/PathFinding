﻿using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Serializers.Decorators
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

        public T DeserializeFrom(Stream stream)
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
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(T graph, Stream stream)
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
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            algorithm.Dispose();
        }
    }
}