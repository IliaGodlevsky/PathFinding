using Activating.Comparers;
using Activating.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using static System.Linq.Expressions.Expression;

using ActivatorsSet = System.Collections.Generic.Dictionary<System.Type[], System.Delegate>;

namespace Activating
{
    public static class ObjectActivator
    {
        static ObjectActivator()
        {
            var comparer = new TypeEqualityComparer();
            activators = new Dictionary<Type, ActivatorsSet>(comparer);
        }

        /// <summary>
        /// Creates a <see cref="Delegate"/>, that represents lambda 
        /// expression, from constructor <paramref name="ctor"/>
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="ctor"></param>
        /// <returns>A <see cref="Delegate"/>, that represents a 
        /// lambda expression, from constructor <paramref name="ctor"/>, that can 
        /// create an instance, that can be assigned to variable 
        /// of <typeparamref name="TReturnType"/> type</returns>
        public static ActivatorHandler<TReturnType> CreateActivator<TReturnType>(ConstructorInfo ctor) 
            where TReturnType : class
        {
            var ctorParameters = ctor.GetParameters();
            var parameter = Parameter(typeof(object[]), "args");
            var argsExpression = CreateArgumentExpressions(ctorParameters, parameter);
            var newExpression = New(ctor, argsExpression);
            var lambda = Lambda(typeof(ActivatorHandler<TReturnType>), newExpression, parameter);

            return (ActivatorHandler<TReturnType>)lambda.Compile();
        }

        public static void RegisterConstructors<TReturnType>(Type type) 
            where TReturnType : class
        {
            foreach (var ctor in type.GetConstructors())
            {
                RegisterConstructor<TReturnType>(ctor);
            }
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
        public static bool RegisterConstructor<TReturnType>(ConstructorInfo ctor) 
            where TReturnType : class
        {
            if (ctor == null)
            {
                throw new ArgumentNullException(nameof(ctor));
            }

            if (ctor.DeclaringType.IsAbstract)
            {
                throw new ArgumentException("Can't register constructor of abstract type");
            }

            var typeToActivate = ctor.DeclaringType;
            if (CanCreateActivator<TReturnType>(typeToActivate))
            {
                var activator = CreateActivator<TReturnType>(ctor);
                CreateActivatorsSetIfDoesntExist(typeToActivate);
                var ctorParams = ctor.GetParameters().Select(param => param.ParameterType).ToArray();
                AddActivatorIfDoesntExist(typeToActivate, ctorParams, activator);
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
        public static Delegate GetRegisteredActivator(Type type, params object[] ctorParams)
        {
            if (activators.TryGetValue(type, out var typeActivators))
            {
                var paramTypes = ToTypesArray(ctorParams);

                if (typeActivators.TryGetValue(paramTypes, out var activator))
                {
                    return activator;
                }
            }

            throw new KeyNotFoundException("For this type activator doesn't have any constructor");
        }

        public static TResultType CreateInstance<TResultType>(Type type, 
            params object[] arguments) 
            where TResultType : class
        {
            var activator = (ActivatorHandler<TResultType>)GetRegisteredActivator(type, arguments);
            return activator(arguments);
        }

        private static IEnumerable<Expression> CreateArgumentExpressions(
            ParameterInfo[] ctorParametres,
            ParameterExpression parameter)
        {
            for (int i = 0; i < ctorParametres.Length; i++)
            {
                var index = Constant(i);
                var parameterType = ctorParametres[i].ParameterType;
                var parameterAccessorExpression = ArrayIndex(parameter, index);
                var parameterCastExpression = Convert(parameterAccessorExpression, parameterType);
                yield return parameterCastExpression;
            }
        }

        private static bool CreateActivatorsSetIfDoesntExist(Type typeToActivate)
        {
            if (!activators.TryGetValue(typeToActivate, out _))
            {
                var comparer = new ArrayOfTypesEqualityComparer();
                activators.Add(typeToActivate, new ActivatorsSet(comparer));
                return true;
            }
            return false;
        }

        private static bool AddActivatorIfDoesntExist(Type typeToActivate, 
            Type[] ctorParams, Delegate activator)
        {
            if (!activators[typeToActivate].TryGetValue(ctorParams, out _))
            {
                activators[typeToActivate].Add(ctorParams, activator);
                return true;
            }

            return false;
        }

        private static bool CanCreateActivator<TReturnType>(Type typeToActivate) where TReturnType : class
        {
            return typeof(TReturnType).IsAssignableFrom(typeToActivate);
        }

        private static Type[] ToTypesArray(params object[] ctorParams)
        {
            return ctorParams.Select(param => param.GetType()).ToArray();
        }

        /// <summary>
        /// A dictionary of registed activators for objects
        /// </summary>
        private static readonly Dictionary<Type, ActivatorsSet> activators;
    }
}
