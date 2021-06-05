using Conditional.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction if else if
    /// </summary>
    public sealed class If<T> : IDisposable
    {
        /// <summary>
        /// Creates a new if construction with <paramref name="condition"/>
        /// as condition and <paramref name="body"/> as body of condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        public If(Predicate<T> condition, Action<T> body) 
            : this(new ConditionConstruction<T>(body, condition))
        {
            
        }

        public If(IConditionConstruction<T> conditionConstruction) : this()
        {
            ElseIf(conditionConstruction);
        }

        public If()
        {
            conditionConstructions = new List<IConditionConstruction<T>>();
            hasElseConstruction = false;
        }

        /// <summary>
        /// Adds a new condition construction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when add 'else if' statement after else construction</exception>
        /// <exception cref="ArgumentNullException"/>
        public If<T> ElseIf(Predicate<T> condition, Action<T> body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            return ElseIf(new ConditionConstruction<T>(body, condition));
        }

        public If<T> ElseIf(IConditionConstruction<T> conditionConstruction)
        {
            if (!hasElseConstruction)
            {
                conditionConstructions.Add(conditionConstruction);
                return this;
            }

            throw new InvalidOperationException(ExceptionMessage);
        }

        /// <summary>
        /// Add a new condition, that will throw exception if condition is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="condition"></param>
        /// <param name="exceptionParams"></param>
        /// <returns></returns>
        public If<T> ElseIfThrow<TException>(Predicate<T> condition, params object[] exceptionParams)
            where TException : Exception
        {
            return ElseIf(new ExceptionConditionConstruction<TException, T>(condition, exceptionParams));
        }

        public If<T> ElseThrow<TException>(params object[] exceptionParams)
            where TException : Exception
        {
            var self = ElseIfThrow<TException>(null, exceptionParams);
            hasElseConstruction = true;
            return self;
        }

        /// <summary>
        /// Adds a new condition construction
        /// without condition (e.a. else)
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when 'else' statement adding after else statement</exception>
        /// /// <exception cref="ArgumentNullException"/>
        public If<T> Else(Action<T> body)
        {
            var temp = ElseIf(null, body);
            hasElseConstruction = true;
            return temp;
        }

        /// <summary>
        /// Walks through all condition constructions and
        /// executes first executable condition
        /// </summary>
        /// <param name="parametre"></param>
        /// <param name="walkCondition"></param>
        public If<T> WalkThroughConditions(T parametre, Predicate<T> walkCondition = null)
        {
            if (walkCondition == null || walkCondition?.Invoke(parametre) == true)
            {
                bool IsCondition(IConditionConstruction<T> condition)
                {
                    return condition.IsCondition(parametre) == true;
                }

                conditionConstructions
                    .FirstOrDefault(IsCondition)
                    ?.ExecuteBody(parametre);
            }
            return this;
        }

        public void Dispose()
        {
            var disposables = conditionConstructions.OfType<IDisposable>();
            Array.ForEach(disposables.ToArray(), item => item.Dispose());
            conditionConstructions.Clear();
        }

        private bool hasElseConstruction;
        private readonly ICollection<IConditionConstruction<T>> conditionConstructions;

        private const string ExceptionMessage = "Couldn't add 'if else' statement after 'else' statement";
    }
}
