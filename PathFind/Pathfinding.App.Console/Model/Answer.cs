using Pathfinding.App.Console.Localization;
using Shared.Primitives.ValueRange;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Answer : IComparable<Answer>, IEquatable<Answer>
    {
        public static readonly Answer Yes = new(1, Languages.Yes);
        public static readonly Answer No = new(0, Languages.No);
        private static readonly Answer Default = new(-1, string.Empty);

        public static readonly InclusiveValueRange<Answer> Range = new(Yes, No);

        public static readonly IReadOnlyCollection<Answer> Answers = new[] { Yes, No };

        private readonly int value;
        private readonly string display;
        private readonly int hash;

        private Answer(int value, string display)
        {
            this.display = display;
            this.value = value;
            this.hash = HashCode.Combine(value, display);
        }

        public int CompareTo(Answer other)
        {
            return value.CompareTo(other.value);
        }

        public bool Equals(Answer other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Answer answer ? Equals(answer) : false;
        }

        public override string ToString()
        {
            return display;
        }

        public override int GetHashCode()
        {
            return hash;
        }

        public static bool TryParse(string input, out Answer result)
        {
            var ignoreCase = StringComparison.OrdinalIgnoreCase;
            if (int.TryParse(input, out int value))
            {
                result = value;
                return !result.Equals(Default);
            }
            result = input.Equals(Yes, ignoreCase)
                ? Yes : input.Equals(No, ignoreCase) ? No : Default;
            return !result.Equals(Default);
        }

        public static implicit operator bool(Answer answer)
        {
            return answer.Equals(Yes);
        }

        public static implicit operator int(Answer answer)
        {
            return answer.value;
        }

        public static implicit operator Answer(int value)
        {
            return value == Yes ? Yes : value == No ? No : Default;
        }

        public static implicit operator string(Answer answer)
        {
            return answer.display;
        }
    }
}