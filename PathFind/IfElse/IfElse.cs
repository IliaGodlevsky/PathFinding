using IfElse.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IfElse
{
    public sealed class IfElse<T> : _IfElse<T>
    {
        public IfElse(If<T> condition) : this()
        {
            conditions.Add(condition);
        }

        public IfElse(Predicate<T> condition,
            Action<T> body) : this()
        {
            conditions.Add(new If<T>(condition, body));
        }

        public void Walk(T parametre)
        {
            bool IsCondition(_If<T> _if)
                => _if.IsCondition(parametre) == true;

            conditions
                ?.FirstOrDefault(IsCondition)
                ?.ExecuteBody(parametre);
        }

        public _IfElse<T> ElseIf(Predicate<T> condition,
            Action<T> body)
        {
            return ElseIf(new If<T>(condition, body));
        }

        public _IfElse<T> ElseIf(_If<T> condition)
        {
            conditions.Add(condition);
            return this;
        }

        private IfElse()
        {
            conditions = new List<_If<T>>();
        }

        private readonly List<_If<T>> conditions;
    }
}
