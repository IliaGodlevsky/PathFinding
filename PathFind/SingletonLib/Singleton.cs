using Common.Extensions;
using SingletonLib.Exceptions;
using System;
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
        private static readonly string Message = $"{typeof(T).Name} has neither private nor protected parametreless constructor";

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

        static Singleton() => instance = new Lazy<T>(CreateInstance, true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T CreateInstance()
        {
            return typeof(T).TryGetNonPublicParametrelessCtor(out var ctor)
                ? (T)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(Message);
        }
    }
}
