using System;
using System.Collections.Generic;
using System.Linq;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction if else
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class If<T>
    {
        /// <summary>
        /// Creates a new if construction with <paramref name="condition"/>
        /// as condition and <paramref name="body"/> as body of condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        public If(Predicate<T> condition, Action<T> body) : this()
        {
            conditinConstructions.Add(new ConditionConstruction<T>(body, condition));
        }

        /// <summary>
        /// Adds a new <see cref="If{T}"></see> condition construction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public If<T> ElseIf(Predicate<T> condition, Action<T> body)
        {
            conditinConstructions.Add(new ConditionConstruction<T>(body, condition));
            return this;
        }

        /// <summary>
        /// Adds a new <see cref="If{T}"></see> condition construction
        /// without condition (e.a. else)
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public If<T> Else(Action<T> body)
        {
            if (!hasElseConstruction)
            {
                hasElseConstruction = true;
                return ElseIf(null, body);
            }
            return this;
        }

        /// <summary>
        /// Walks through all condition constructions and
        /// executes first executable condition
        /// </summary>
        /// <param name="paramtre"></param>
        public void Walk(T paramtre, Predicate<T> walkCondition = null)
        {
            bool IsCondition(ConditionConstruction<T> condition)
                => condition.IsCondition(paramtre) == true;

            Action<T> body = param => conditinConstructions
                                        .FirstOrDefault(IsCondition)
                                        ?.ExecuteBody(param);

            var conditionConstruction 
                = new ConditionConstruction<T>(body, walkCondition);

            if (IsCondition(conditionConstruction))
            {
                conditionConstruction?.ExecuteBody(paramtre);
            }
        }

        private If()
        {
            conditinConstructions = new List<ConditionConstruction<T>>();
            hasElseConstruction = false;
        }

        private bool hasElseConstruction;
        private readonly List<ConditionConstruction<T>> conditinConstructions;
    }
}
