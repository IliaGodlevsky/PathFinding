using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console
{
    internal static class Tokens
    {
        public static readonly IToken Main = new Token();
        public static readonly IToken Path = new Token();
        public static readonly IToken Graph = new Token();
        public static readonly IToken AppLayout = new Token();
        public static readonly IToken Common = new Token();
        public static readonly IToken History = new Token();
        public static readonly IToken Target = new Token();
        public static readonly IToken Transit = new Token();
        public static readonly IToken Visited = new Token();
        public static readonly IToken Enqueued = new Token();
        public static readonly IToken Crossed = new Token();
        public static readonly IToken Obstacle = new Token();
        public static readonly IToken Regular = new Token();
        public static readonly IToken Source = new Token();
        public static readonly IToken Statistics = new Token();
        public static readonly IToken Visualization = new Token();
        public static readonly IToken Pathfinding = new Token();

        public static IToken Bind(Func<bool> condition, IToken token) 
            => new ConditionToken(condition, token);

        private sealed class Token : IToken
        {
            private readonly Guid token;

            public Token()
            {
                token = Guid.NewGuid();
            }

            public bool Equals(IToken other)
            {
                return Equals((object)other);
            }

            public override bool Equals(object obj)
            {
                return obj.GetHashCode() == GetHashCode();
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