using Moq;

namespace Pathfinding.ConsoleApp.Tests
{
    [TypeMatcher]
    internal sealed class IsAnyToken : ITypeMatcher, IEquatable<IsAnyToken>
    {
        public bool Matches(Type typeArgument) => true;

        public bool Equals(IsAnyToken other) => true;
    }
}
