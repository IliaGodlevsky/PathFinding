using System;

namespace IfElse.Interface
{
    public interface _IfElse<T>
    {
        void Walk(T parametre);

        _IfElse<T> ElseIf(Predicate<T> condition,
            Action<T> body);

        _IfElse<T> ElseIf(_If<T> condition);
    }
}
