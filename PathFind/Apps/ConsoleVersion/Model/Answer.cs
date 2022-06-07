using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using ValueRange;

namespace ConsoleVersion.Model
{
    internal sealed class Answer : IComparable
    {
        public static readonly Answer None = new Answer(-1, string.Empty);

        public static readonly Answer Yes = new Answer(1, "Yes");

        public static readonly Answer No = new Answer(0, "No");

        public static readonly InclusiveValueRange<Answer> Range = new InclusiveValueRange<Answer>(Yes, No);

        private static readonly Dictionary<int, Answer> valueMap = new Dictionary<int, Answer>() { { 1, Yes}, { 0, No} };
        private static readonly Dictionary<string, Answer> nameMap = new Dictionary<string, Answer>() { { "yes", Yes }, { "no", No } };

        private readonly int value;
        private readonly string name;

        private Answer(int value, string name)
        {            
            this.value = value;
            this.name = name;
        }

        public int CompareTo(object obj)
        {
            if (obj is Answer answer)
            {
                return answer.value.CompareTo(value);
            }
            throw new ArgumentException("object must be answer");
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case int value:
                    return value == this.value;
                case string name:
                    return name.Equals(this.name, StringComparison.InvariantCultureIgnoreCase) || this.value.Equals(name);
                case Answer answer:
                    return answer == this;
                default:
                    return false;
            }
        }

        public override string ToString() => name;

        public static bool operator ==(Answer value1, Answer value2)
        {
            return value1.value == value2.value && value1.name == value2.name;
        }

        public static bool operator !=(Answer value1, Answer value2)
        {
            return !(value1 == value2);
        }

        public static explicit operator bool(Answer answer)
        {
            return answer.value > 0;
        }

        public static explicit operator Answer(int answer)
        {
            return valueMap.GetOrDefault(answer, () => Answer.None);
        }

        public static explicit operator Answer(string answer)
        {
            return nameMap.GetOrDefault(answer.ToLower(), () => Answer.None);
        }
    }
}