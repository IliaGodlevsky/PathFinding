using SingletonLib.Exceptions;
using System;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace SingletonLib
{
    public abstract class Singleton<TInstance, TInterface>
        where TInstance : Singleton<TInstance, TInterface>, TInterface
    {
        private static readonly Lazy<TInstance> instance;

        public static TInterface Interface => Instance;

        public static TInstance Instance => instance.Value;

        static Singleton()
        {
            instance = new Lazy<TInstance>(CreateInstance, true);
        }

        private static TInstance CreateInstance()
        {
            var instanceType = typeof(TInstance);
            var ctor = instanceType
                .GetConstructors(NonPublic | BindingFlags.Instance)
                .FirstOrDefault(c => !c.GetParameters().Any());
            return ctor != null
                ? (TInstance)ctor.Invoke(Array.Empty<object>())
                : throw new SingletonException(instanceType);
        }
    }
}