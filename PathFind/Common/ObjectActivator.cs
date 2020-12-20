using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
    public delegate TReturnType Activator<TReturnType>(params object[] args) where TReturnType : class;

    public static class ObjectActivator
    {
        static ObjectActivator()
        {
            Activators = new Dictionary<Type, Delegate>();
        }

        public static bool RegisterConstructor<TReturnType>(ConstructorInfo ctor) where TReturnType : class
        {
            if (ctor == null)
            {
                throw new ArgumentNullException(nameof(ctor));
            }

            if (ctor.DeclaringType.IsAbstract)
            {
                throw new ArgumentException("Can't register constructor of abstract type");
            }

            if (typeof(TReturnType).IsAssignableFrom(ctor.DeclaringType))
            {
                var activator = CreateActivator<TReturnType>(ctor);
                Activators[ctor.DeclaringType] = activator;
                return true;
            }

            return false;
        }

        public static Delegate GetActivator(Type type)
        {
            if (Activators.TryGetValue(type, out Delegate activator))
            {
                return activator;
            }

            throw new KeyNotFoundException("For this type activator doesn't have any constructor");
        }

        public static Activator<T> GetActivator<T>() where T : class
        {
            return (Activator<T>)GetActivator(typeof(T));
        }

        private static Dictionary<Type, Delegate> Activators { get; set; }

        private static Delegate CreateActivator<TReturnType>(ConstructorInfo ctor) where TReturnType : class
        {
            var ctorParameters = ctor.GetParameters();
            var parameter = Expression.Parameter(typeof(object[]), "args");
            var argsExpression = new Expression[ctorParameters.Length];

            for (int i = 0; i < ctorParameters.Length; i++)
            {
                var index = Expression.Constant(i);
                var parameterType = ctorParameters[i].ParameterType;
                var parameterAccessorExpression
                    = Expression.ArrayIndex(parameter, index);
                var parameterCastExpression = Expression
                    .Convert(parameterAccessorExpression, parameterType);
                argsExpression[i] = parameterCastExpression;
            }

            var newExpression = Expression.New(ctor, argsExpression);
            var lambda = Expression.Lambda(typeof(Activator<TReturnType>),
                newExpression, parameter);

            return lambda.Compile();
        }
    }
}
