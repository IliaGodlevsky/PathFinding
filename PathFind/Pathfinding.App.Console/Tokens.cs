using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console
{
    internal class Tokens : IToken
    {
        public static readonly IToken Main = new Tokens();
        public static readonly IToken Path = new Tokens();
        public static readonly IToken Graph = new Tokens();
        public static readonly IToken AppLayout = new Tokens();
        public static readonly IToken Common = new Tokens();
        public static readonly IToken History = new Tokens();
        public static readonly IToken Target = new Tokens();
        public static readonly IToken Transit = new Tokens();
        public static readonly IToken Visited = new Tokens();
        public static readonly IToken Enqueued = new Tokens();
        public static readonly IToken Crossed = new Tokens();
        public static readonly IToken Obstacle = new Tokens();
        public static readonly IToken Regular = new Tokens();
        public static readonly IToken Source = new Tokens();
        public static readonly IToken Statistics = new Tokens();
        public static readonly IToken Visualization = new Tokens();
        public static readonly IToken Pathfinding = new Tokens();

        public static IToken Bind(Func<bool> condition, IToken token) 
            => new ConditionToken(condition, token);

        private readonly int hashCode;

        private Tokens()
        {
            hashCode = Guid.NewGuid().GetHashCode();
        }

        public bool Equals(IToken other)
        {
            return other.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            return hashCode.ToString();
        }

        private sealed class ConditionToken : IToken
        {
            private readonly IToken token;
            private readonly Func<bool> condition;

            public ConditionToken(Func<bool> condition, IToken token)
            {
                this.condition = condition;
                this.token = token;
            }

            public override bool Equals(object obj)
            {
                return GetHashCode() == obj.GetHashCode() && condition();
            }

            public bool Equals(IToken other)
            {
                return Equals((object)other);
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
}