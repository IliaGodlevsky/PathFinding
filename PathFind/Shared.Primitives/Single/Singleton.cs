﻿using Shared.Primitives.Single.Exceptions;
using System;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Shared.Primitives.Single
{
    /// <summary>
    /// A base class of singleton classes
    /// </summary>
    /// <typeparam name="TInstance">A type, that will be representing 
    /// an <see cref="Instance"/> property</typeparam>
    /// <typeparam name="TInterface">An interface, that will be representing
    /// an <see cref="Interface"/> property</typeparam>
    public abstract class Singleton<TInstance, TInterface>
        where TInstance : Singleton<TInstance, TInterface>, TInterface
    {
        private static readonly Lazy<TInstance> instance;

        private Guid SingletonId { get; } = Guid.NewGuid();

        /// <summary>
        /// A property, that contains an instance of type, 
        /// but returns only a main interface of the type
        /// </summary>
        /// <exception cref="SingletonException"> if there is 
        /// no private parametreless constructor</exception>
        public static TInterface Interface => Instance;

        /// <summary>
        /// A property, that contains an instance of type
        /// </summary>
        /// <exception cref="SingletonException"> if there is 
        /// no private parametreless constructor</exception>
        public static TInstance Instance => instance.Value;

        static Singleton()
        {
            instance = new(CreateInstance, true);
        }

        public override bool Equals(object obj)
        {
            return obj is TInstance
                && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return SingletonId.GetHashCode();
        }

        private static TInstance CreateInstance()
        {
            var instanceType = typeof(TInstance);
            var ctor = instanceType
                .GetConstructors(NonPublic | BindingFlags.Instance)
                .SingleOrDefault(c => !c.GetParameters().Any());
            return ctor != null
                ? (TInstance)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(instanceType);
        }
    }
}