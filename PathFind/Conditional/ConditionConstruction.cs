using Conditional.Interfaces;
using System;

namespace Conditional
{
    /// <summary>
    /// Represents a condition construction 'if'
    /// </summary>
    internal sealed class ConditionConstruction<T>
        : IConditionConstruction<T>, IDisposable
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
            body?.Invoke(paramtre);
        }

        public void Dispose()
        {
            condition = null;
            body = null;
        }

        private Predicate<T> condition;
        private Action<T> body;
    }
}
