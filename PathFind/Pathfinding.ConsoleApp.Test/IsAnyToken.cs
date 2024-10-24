using Moq;

namespace Pathfinding.ConsoleApp.Test
{
    [TypeMatcher]
    internal sealed class IsAnyToken : ITypeMatcher, IEquatable<IsAnyToken>
    {
        public bool Equals(IsAnyToken? other) => true;

        public bool Matches(Type typeArgument) => true;
    }
}
