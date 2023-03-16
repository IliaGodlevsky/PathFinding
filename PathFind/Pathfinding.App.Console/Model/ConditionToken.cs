using System;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConditionToken
    {
        public static ConditionToken Create(Func<bool> condition, Tokens token) => new(condition, token);

        private readonly Tokens token;
        private readonly Func<bool> condition;

        private ConditionToken(Func<bool> condition, Tokens token)
        {
            this.condition = condition;
            this.token = token;
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj?.GetHashCode()
                && condition?.Invoke() == true;
        }

        public override int GetHashCode()
        {
            return token.GetHashCode();
        }

        public static implicit operator Tokens(ConditionToken token)
        {
            return token.token;
        }

        public override string ToString()
        {
            return token.ToString();
        }
    }
}
