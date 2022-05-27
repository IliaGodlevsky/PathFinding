using Common.Extensions;
using SingletonLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingletonLib
{
    public abstract class Singleton<TInstance, TInterface>
        where TInstance : class, TInterface
    {
        private static readonly Lazy<TInterface> instance;

        public static TInterface Instance => instance.Value;

        static Singleton()
        {
            instance = new Lazy<TInterface>(() => CreateInstance(typeof(TInstance)), true);
        }

        public static IReadOnlyList<TInterface> GetMany(int count)
        {
            return count > 0
                ? Enumerable.Repeat(Instance, count).ToArray()
                : Array.Empty<TInterface>();
        }

        private static TInstance CreateInstance(Type ofType)
        {
            return ofType.TryGetNonPublicParametrelessCtor(out var ctor)
                ? (TInstance)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(ofType);
        }
    }
}