using Pathfinding.App.Console.Localization;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed record class Answer : IComparable<Answer>
    {
        public static readonly Answer Yes = new(1, Languages.Yes);
        public static readonly Answer No = new(0, Languages.No);
        private static readonly Answer Default = new(-1, string.Empty);

        public static readonly InclusiveValueRange<Answer> Range = new(Yes, No);

        private readonly int value;
        private readonly string display;

        private Answer(int value, string display)
        {
            this.display = display;
            this.value = value;
        }

        public int CompareTo(Answer other)
        {
            return value.CompareTo(other.value);
        }

        public override string ToString()
        {
            return display;
        }

        public static bool TryParse(string input, out Answer result)
        {
            var ignoreCase = StringComparison.OrdinalIgnoreCase;
            if (int.TryParse(input, out int value))
            {
                result = value == Yes.value ? 
                    Yes : value == No.value ? No : Default;
                return !result.Equals(Default);
            }
            result = input.Equals(Yes.display, ignoreCase)
                ? Yes : input.Equals(No.display, ignoreCase) ? No : Default;
            return !result.Equals(Default);
        }

        public static implicit operator bool(Answer answer)
        {
            return answer.Equals(Yes);
        }
    }
}