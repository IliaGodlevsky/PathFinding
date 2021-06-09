using Conditional.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conditional
{
    /// <summary>
    /// Represents a condition constructions collection
    /// </summary>
    public sealed class Conditional<T> : IDisposable
    {
        /// <summary>
        /// Creates a new conditional with <paramref name="condition"/>
        /// as condition and <paramref name="body"/> as body of condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        public Conditional(Action<T> body, Predicate<T> condition = null)
            : this(new ConditionConstruction<T>(body, condition))
        {

        }

        public Conditional(IConditionConstruction<T> conditionConstruction) : this()
        {
            PerformIf(conditionConstruction);
        }

        public Conditional()
        {
            conditionConstructions = new List<IConditionConstruction<T>>();
        }

        /// <summary>
        /// Adds a new condition construction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        /// <returns></returns>      
        /// <exception cref="ArgumentNullException"/>
        public Conditional<T> PerformIf(Action<T> body, Predicate<T> condition = null)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            return PerformIf(new ConditionConstruction<T>(body, condition));
        }

        /// <summary>
        /// Adds a new condition contruction class/ struct
        /// </summary>
        /// <param name="conditionConstruction"></param>
        /// <returns></returns>
        public Conditional<T> PerformIf(IConditionConstruction<T> conditionConstruction)
        {
            conditionConstructions.Add(conditionConstruction);
            return this;
        }

        /// <summary>
        /// Add a new condition construction, that will throw an exception if the condition is true
        /// </summary>
        /// <typeparam name="TException">A type of the exception to be thrown</typeparam>
        /// <param name="condition">a delegate that represents a condition 
        /// that, if true, will cause an exception to be thrown</param>
        /// <param name="exceptionParams">An array of arguments that match in number,
        /// order, and type the parameters of the constructor of the <typeparamref name="TException"/></param>
        /// <returns></returns>
        public Conditional<T> ThrowIf<TException>(Predicate<T> condition = null, params object[] exceptionParams)
            where TException : Exception
        {
            return PerformIf(new ExceptionConditionConstruction<TException, T>(condition, exceptionParams));
        }

        /// <summary>
        /// Walks through all condition constructions and
        /// executes first executable condition
        /// </summary>
        /// <param name="parametre"></param>
        /// <param name="walkCondition"></param>
        public Conditional<T> PerformFirstSuitable(T parametre, Predicate<T> walkCondition = null)
        {
            if (walkCondition == null || walkCondition?.Invoke(parametre) == true)
            {
                bool IsCondition(IConditionConstruction<T> condition)
                {
                    return condition.IsCondition(parametre) == true;
                }

                conditionConstructions.FirstOrDefault(IsCondition)?.ExecuteBody(parametre);
            }
            return this;
        }

        /// <summary>
        /// Walks through al condition contructions 
        /// and executes all of them one by one
        /// </summary>
        /// <param name="parametre"></param>
        /// <param name="walkCondition"></param>
        /// <returns></returns>
        public Conditional<T> PerformAll(T parametre, Predicate<T> walkCondition = null)
        {
            if (walkCondition == null || walkCondition?.Invoke(parametre) == true)
            {
                bool IsCondition(IConditionConstruction<T> condition)
                {
                    return condition.IsCondition(parametre) == true;
                }

                void Execute(IConditionConstruction<T> condition)
                {
                    condition.ExecuteBody(parametre);
                }

                var suitable = conditionConstructions.Where(IsCondition);
                Array.ForEach(suitable.ToArray(), Execute);
            }
            return this;
        }

        /// <summary>
        /// Clears the <see cref="Conditional{T}"/> class
        /// </summary>
        public void Dispose()
        {
            var disposables = conditionConstructions.OfType<IDisposable>();
            Array.ForEach(disposables.ToArray(), item => item.Dispose());
            conditionConstructions.Clear();
        }

        private readonly ICollection<IConditionConstruction<T>> conditionConstructions;
    }
}
