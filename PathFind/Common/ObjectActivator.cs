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
            activators = new Dictionary<Type, Delegate>();
        }

        /// <summary>
        /// Creates a <see cref="Delegate"/>, that represents lambda 
        /// expression, from constructor <paramref name="ctor"/>
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="ctor"></param>
        /// <returns>A <see cref="Delegate"/>, that represents a 
        /// lambda expression, from constructor <paramref name="ctor"/> that can 
        /// create an instance of <typeparamref name="TReturnType"/></returns>
        public static Delegate CreateActivator<TReturnType>(ConstructorInfo ctor) where TReturnType : class
        {
            var ctorParameters = ctor.GetParameters();
            var parameter = Expression.Parameter(typeof(object[]), "args");
            var argsExpression = CreateArgumentExpressions(ctorParameters, parameter);
            var newExpression = Expression.New(ctor, argsExpression);
            var lambda = Expression.Lambda(typeof(ActivatorHandler<TReturnType>), newExpression, parameter);

            return lambda.Compile();
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
                activators[ctor.DeclaringType] = activator;
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
        public static Delegate GetRegisteredActivator(Type type)
        {
            if (activators.TryGetValue(type, out Delegate activator))
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
            return (ActivatorHandler<T>)GetRegisteredActivator(typeof(T));
        }

        private static IEnumerable<Expression> CreateArgumentExpressions(
            ParameterInfo[] ctorParametres,
            ParameterExpression parameter)
        {
            for (int i = 0; i < ctorParametres.Length; i++)
            {
                var index = Expression.Constant(i);
                var parameterType = ctorParametres[i].ParameterType;
                var parameterAccessorExpression = Expression.ArrayIndex(parameter, index);
                var parameterCastExpression = Expression.Convert(parameterAccessorExpression, parameterType);
                yield return parameterCastExpression;
            }
        }

        /// <summary>
        /// A dictionary of registed activators for objects
        /// </summary>
        private static readonly IDictionary<Type, Delegate> activators;
    }
}
