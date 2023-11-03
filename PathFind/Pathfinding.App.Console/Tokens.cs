using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static readonly IToken Visual = new Token();
        public static readonly IToken Statistics = new Token();
        public static readonly IToken Visualization = new Token();
        public static readonly IToken Pathfinding = new Token();

        public static IToken Bind(this IToken token, Func<bool> condition)
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
            private readonly Lazy<List<Func<bool>>> condition;

            public ConditionToken(Func<bool> condition, IToken token)
            {
                this.condition = new(() => Disassemble(condition));
                this.token = token;
            }

            public override bool Equals(object obj)
            {
                return GetHashCode() == obj.GetHashCode()
                    && IsValidCondition();
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

            private bool IsValidCondition()
            {
                return condition.Value.All(c => c.Invoke());
            }

            private static List<Func<bool>> Disassemble(Func<bool> condition)
            {
                var list = condition.GetInvocationList()
                    .OfType<Func<bool>>()
                    .ToList();
                if (list.Count > 1)
                {
                    foreach (var del in list)
                    {
                        list.AddRange(Disassemble(del));
                    }
                }
                return list;
            }
        }
    }
}