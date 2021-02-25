using IfElse.Interface;
using System;

namespace IfElse
{
    public sealed class If<T> : _If<T>
    {
        public If(Predicate<T> condition = null,
            Action<T> body = null)
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

        private readonly Predicate<T> condition;
        private readonly Action<T> body;
    }
}
