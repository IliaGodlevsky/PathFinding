using System;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction 'if'
    /// </summary>
    internal class ConditionConstruction<T>
    {
        public ConditionConstruction(Action<T> body,
            Predicate<T> condition = null)
        {
            this.condition = condition;
            this.body = body;
        }

        public bool? IsCondition(T parametre)
        {
            return condition == null
                || condition?.Invoke(parametre) == true;
        }

        public void ExecuteBody(T paramtre)
        {
            body(paramtre);
        }

        private readonly Predicate<T> condition;
        private readonly Action<T> body;
    }
}
