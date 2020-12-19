using Common.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace Common
{
    public delegate TSource Activator<TSource>(params object[] args) where TSource : class;

    public static class ObjectActivator
    {
        static ObjectActivator()
        {
            Activators = new Dictionary<Type, Delegate>();
        }

        public static bool RegisterConstructor<TSource>(ConstructorInfo ctor) where TSource : class
        {
            if (ctor == null)
            {
                throw new ArgumentNullException("Incoming argument is null");
            }

            if (CanBeAdded<TSource>(ctor))
            {
                var activator = CreateActivator<TSource>(ctor);
                Activators.Add(ctor.DeclaringType, activator);
                return true;
            }

            return false;
        }

        public static Delegate GetConstructor(Type type)
        {
            if (Activators.TryGetValue(type, out Delegate activator))
            {
                return activator;
            }

            throw new KeyNotFoundException("For this type activator doesn't have any constructor");
        }

        private static Dictionary<Type, Delegate> Activators { get; set; }

        private static Delegate CreateActivator<TSource>(ConstructorInfo ctor) where TSource : class
        {
            var ctorParameters = ctor.GetParameters();
            var parameter = Expression.Parameter(typeof(object[]), "args");
            var argsExpression = new Expression[ctorParameters.Length];
            for (int i = 0; i < ctorParameters.Length; i++)
            {
                var index = Expression.Constant(i);
                var parameterType = ctorParameters[i].ParameterType;
                var parameterAccessorExpression = Expression.ArrayIndex(parameter, index);
                var parameterCastExpression = Expression.Convert(parameterAccessorExpression, parameterType);
                argsExpression[i] = parameterCastExpression;
            }
            var newExpression = Expression.New(ctor, argsExpression);
            var lambda = Expression.Lambda(typeof(Activator<TSource>), newExpression, parameter);
            return lambda.Compile();
        }

        private static bool CanBeAdded<TSource>(ConstructorInfo ctor) where TSource : class
        {
            return (ctor.DeclaringType.IsImplementationOf<TSource>() 
                || typeof(TSource).Equals(ctor.DeclaringType))
                && !Activators.ContainsKey(ctor.DeclaringType);
        }
    }
}
