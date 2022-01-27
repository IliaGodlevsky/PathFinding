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
    public abstract class Singleton<T> where T : class
    {
        [NonSerialized]
        private static readonly Lazy<T> instance;
        /// <summary>
        /// Returns the instance of <typeparamref name="T"/>.
        /// The instance is always the same object
        /// </summary>
        /// <exception cref="SingletonException"> 
        /// thrown if <typeparamref name="T"/>
        /// doesn't have a private or protected 
        /// paramtreless constructor</exception>
        public static T Instance => instance.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> GetMany(int count)
        {
            return count > 0
                ? Enumerable.Repeat(Instance, count).ToArray()
                : Array.Empty<T>();
        }

        static Singleton()
        {
            instance = new Lazy<T>(() => CreateInstance(typeof(T)), true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T CreateInstance(Type ofType)
        {
            return ofType.TryGetNonPublicParametrelessCtor(out var ctor)
                ? (T)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(Constants.GetMessage(ofType));
        }
    }
}
