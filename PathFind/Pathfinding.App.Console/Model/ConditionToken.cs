using System;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConditionToken
    {
        public static ConditionToken Create(Func<bool> condition, Guid token)
            => new ConditionToken(condition, token);

        private readonly Func<bool> condition;
        private readonly Guid token;

        private ConditionToken(Func<bool> condition, Guid token)
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

        public override string ToString()
        {
            return token.ToString();
        }
    }
}
