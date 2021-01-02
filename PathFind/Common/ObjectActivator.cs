using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// A handler for constructor
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    /// <param name="args"></param>
    /// <returns><typeparamref name="TReturnType"></typeparamref></returns>
    public delegate TReturnType ActivatorHandler<out TReturnType>(params object[] args) where TReturnType : class;

    public static class ObjectActivator
    {
        static ObjectActivator()
        {
            Activators = new Dictionary<Type, Delegate>();
        }

        /// <summary>
        /// Registers constructor that will return <typeparamref name="TReturnType"/>
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="ctor"></param>
        /// <returns><see cref="true"/> when register is successful, <see cref="false"/> when ctor 
        /// declaring type is not assignable to <typeparamref name="TReturnType"/></returns>
        /// <exception cref="ArgumentNullException">Thrown when ctor is null</exception>
        /// <exception cref="ArgumentException">Thrown when ctor declaring type is abstract</exception>
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

        /// <summary>
        /// Returns an activator according to <paramref name="type"></paramref>
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Activator handler for <paramref name="type"></paramref></returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for <paramref name="type"></paramref></exception>
        public static Delegate GetActivator(Type type)
        {
            if (Activators.TryGetValue(type, out Delegate activator))
            {
                return activator;
            }

            throw new KeyNotFoundException("For this type activator doesn't have any constructor");
        }

        /// <summary>
        /// Returns activator for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Activator for <typeparamref name="T"></typeparamref></returns>
        /// <exception cref="KeyNotFoundException">Thrown when activator 
        /// doesn't exist for <typeparamref name="T"/></exception>
        public static ActivatorHandler<T> GetActivator<T>() where T : class
        {
            return (ActivatorHandler<T>)GetActivator(typeof(T));
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
            var lambda = Expression.Lambda(typeof(ActivatorHandler<TReturnType>),
                newExpression, parameter);

            return lambda.Compile();
        }
    }
}
