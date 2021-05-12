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
        /// <exception cref="ArgumentNullException"/>
        public If<T> ElseIf(Predicate<T> condition, Action<T> body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (!hasElseConstruction)
            {
                conditionConstructions.Add(new ConditionConstruction<T>(body, condition));
                return this;
            }

            throw new InvalidOperationException(ExceptionMessage);
        }

        /// <summary>
        /// Adds a new <see cref="If{T}"></see> condition construction
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
        public void Walk(T parametre, Predicate<T> walkCondition = null)
        {
            if (walkCondition == null 
                || walkCondition?.Invoke(parametre) == true)
            {
                bool IsCondition(ConditionConstruction<T> condition)
                {
                    return condition.IsCondition(parametre) == true;
                }

                conditionConstructions
                    .FirstOrDefault(IsCondition)
                    ?.ExecuteBody(parametre);
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
