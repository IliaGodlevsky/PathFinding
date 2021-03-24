using System;
using System.Collections.Generic;
using System.Linq;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction if else if
    /// </summary>
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
            conditionConstructions.Add(new ConditionConstruction<T>(body, condition));
        }

        /// <summary>
        /// Adds a new <see cref="If{T}"></see> condition construction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when add 'else if' statement after else construction</exception>
        public If<T> ElseIf(Predicate<T> condition, Action<T> body)
        {
            if (!hasElseConstruction)
            {
                conditionConstructions.Add(new ConditionConstruction<T>(body, condition));
                return this;
            }

            throw new InvalidOperationException(ExceptionMessage);
        }

        /// <summary>
        /// Adds a new <see cref="If{T}>"></see> condition construction
        /// without condition (e.a. else)
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when 'else' statement adding after else statement</exception>
        public If<T> Else(Action<T> body)
        {
            if (!hasElseConstruction)
            {
                var temp = ElseIf(null, body);
                hasElseConstruction = true;
                return temp;
            }

            throw new InvalidOperationException(ExceptionMessage);
        }

        /// <summary>
        /// Walks through all condition constructions and
        /// executes first executable condition
        /// </summary>
        /// <param name="paramtre"></param>
        public void Walk(T paramtre, Predicate<T> walkCondition = null)
        {
            if (paramtre != null)
            {
                bool IsCondition(ConditionConstruction<T> condition)
                {
                    return condition.IsCondition(paramtre) == true;
                }

                void Execute(T param)
                {
                    conditionConstructions.FirstOrDefault(IsCondition)?.ExecuteBody(param);
                }

                var conditionConstruction = new ConditionConstruction<T>(Execute, walkCondition);

                if (IsCondition(conditionConstruction))
                {
                    conditionConstruction?.ExecuteBody(paramtre);
                }
            }
        }

        private If()
        {
            conditionConstructions = new List<ConditionConstruction<T>>();
            hasElseConstruction = false;
        }

        private bool hasElseConstruction;
        private readonly List<ConditionConstruction<T>> conditionConstructions;

        private const string ExceptionMessage = "Couldn't add 'if" +
                " else' statement after 'else' statement";
    }
}
