using System;
using System.Collections.Generic;
using System.Linq;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction if else if
    /// </summary>
    public sealed class If
    {
        /// <summary>
        /// Creates a new if construction with <paramref name="condition"/>
        /// as condition and <paramref name="body"/> as body of condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        public If(Predicate<object> condition, Action<object> body) : this()
        {
            conditionConstructions.Add(new ConditionConstruction(body, condition));
        }

        /// <summary>
        /// Adds a new <see cref="If"></see> condition construction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when add 'else if' statement after else construction</exception>
        public If ElseIf(Predicate<object> condition, Action<object> body)
        {
            if (!hasElseConstruction)
            {
                conditionConstructions.Add(new ConditionConstruction(body, condition));
                return this;
            }

            throw new InvalidOperationException(ExceptionMessage);
        }

        /// <summary>
        /// Adds a new <see cref="If"></see> condition construction
        /// without condition (e.a. else)
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">thrown 
        /// when 'else' statement adding after else statement</exception>
        public If Else(Action<object> body)
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
        public void Walk(object paramtre, Predicate<object> walkCondition = null)
        {
            bool IsCondition(ConditionConstruction condition)
                => condition.IsCondition(paramtre) == true;

            void Execute(object param) 
                => conditionConstructions
                         .FirstOrDefault(IsCondition)
                         ?.ExecuteBody(param);

            var conditionConstruction 
                = new ConditionConstruction(Execute, walkCondition);

            if (IsCondition(conditionConstruction))
            {
                conditionConstruction?.ExecuteBody(paramtre);
            }
        }

        private If()
        {
            conditionConstructions = new List<ConditionConstruction>();
            hasElseConstruction = false;
        }

        private bool hasElseConstruction;
        private readonly List<ConditionConstruction> conditionConstructions;

        private const string ExceptionMessage = "Couldn't add 'if" +
                " else' statement after 'else' statement";
    }
}
