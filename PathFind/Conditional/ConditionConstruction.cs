using System;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction 'if'
    /// </summary>
    internal class ConditionConstruction
    {
        public ConditionConstruction(Action<object> body, 
            Predicate<object> condition = null)
        {
            this.condition = condition;
            this.body = body;
        }

        public bool? IsCondition(object parametre)
        {
            return condition == null
                || condition?.Invoke(parametre) == true;
        }

        public void ExecuteBody(object paramtre)
        {
            body(paramtre);
        }

        private readonly Predicate<object> condition;
        private readonly Action<object> body;
    }
}
