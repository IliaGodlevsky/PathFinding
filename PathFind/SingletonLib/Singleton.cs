using Common.Extensions;
using SingletonLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SingletonLib
{
    /// <summary>
    /// A base class for all singleton classes.
    /// This class is abstract
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class Singleton<TInstance, TInterface>
        where TInstance : class, TInterface
    {
        [NonSerialized]
        private static readonly Lazy<TInterface> instance;
        /// <summary>
        /// Returns the instance of <typeparamref name="TInstance"/>.
        /// The instance is always the same object
        /// </summary>
        /// <exception cref="SingletonException"> 
        /// thrown if <typeparamref name="T"/>
        /// doesn't have a private or protected 
        /// paramtreless constructor</exception>
        public static TInterface Instance => instance.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<TInterface> GetMany(int count)
        {
            return count > 0
                ? Enumerable.Repeat(Instance, count).ToArray()
                : Array.Empty<TInterface>();
        }

        static Singleton()
        {
            instance = new Lazy<TInterface>(() => CreateInstance(typeof(TInstance)), true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TInstance CreateInstance(Type ofType)
        {
            return ofType.TryGetNonPublicParametrelessCtor(out var ctor)
                ? (TInstance)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(Constants.GetMessage(ofType));
        }
    }
}
